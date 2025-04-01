
using systemeGAB.DataClass.EntityModels;

namespace systemeGAB.DataClass.Services.Managers
{
    public class TransactionManager : ITransactionManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ICompteBancaireManager _compteBancaireManager;

        public TransactionManager(DatabaseContext databaseContext, ICompteBancaireManager compteBancaireManager)
        {
            _databaseContext = databaseContext;
            _compteBancaireManager = compteBancaireManager;
        }

        public async Task<EntityResponse> AddTransaction(AddTransactionRequest request)
        {
            try
            {
                var existingCompteBancaire = await _databaseContext.compteBancaire.FindAsync(request.idCompte);
                if (existingCompteBancaire == null)
                {
                    return new EntityResponse { success = false, message = "Compte Bancaire associé a la transaction est introuvable" };
                }

                Transaction transaction = new Transaction
                {
                    idCompte = request.idCompte,
                    typeTransaction = request.typeTransaction,
                    montant = request.montant,
                    dateHeure = DateTime.Now
                };

                await _databaseContext.transaction.AddAsync(transaction);
                await _databaseContext.SaveChangesAsync();

                if (transaction.idTransaction <= 0)
                {
                    return new EntityResponse { success = false, message = "Echec de la transaction" };
                }

                if (request.typeTransaction == "Retrait")
                {
                    if (existingCompteBancaire.solde < request.montant)
                    {
                        transaction.statut = "Echec";
                        await _databaseContext.SaveChangesAsync();
                        return new EntityResponse { success = false, message = "Solde insuffisant" };
                    }

                    await _compteBancaireManager.UpdateCompte(new UpdateCompteBancaireRequest
                    {
                        numeroCompte = existingCompteBancaire.numeroCompte,
                        solde = existingCompteBancaire.solde - request.montant,
                        typeCompte = existingCompteBancaire.typeCompte
                    }, request.idCompte);
                }
                else if (request.typeTransaction == "Depot")
                {
                    await _compteBancaireManager.UpdateCompte(new UpdateCompteBancaireRequest
                    {
                        numeroCompte = existingCompteBancaire.numeroCompte,
                        solde = existingCompteBancaire.solde + request.montant,
                        typeCompte = existingCompteBancaire.typeCompte
                    }, request.idCompte);
                }

                transaction.statut = "Reussi";

                await _databaseContext.SaveChangesAsync();

                return new EntityResponse { success = true, message = "Transaction ajoutée avec succès!", transactionResponse = transaction };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de l'ajout de la transaction : " + ex.Message };
            }
        }

        public async Task<EntityResponse> GetTransactionById(int idTransaction)
        {
            try
            {
                var transaction = await _databaseContext.transaction
                    .Where(t => t.idTransaction == idTransaction)
                    .Select(t => new Transaction
                    {
                        idTransaction = t.idTransaction,
                        idCompte = t.idCompte,
                        typeTransaction = t.typeTransaction,
                        montant = t.montant,
                        dateHeure = t.dateHeure,
                        statut = t.statut
                    })
                    .FirstOrDefaultAsync();

                if (transaction == null)
                {
                    return new EntityResponse { success = false, message = "Transaction non trouvée!" };
                }

                return new EntityResponse { success = true, message = "Transaction trouvée!", transactionResponse = transaction };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la recherche de la transaction : " + ex.Message };
            }
        }

        public async Task<List<EntityResponse>> GetTransactions(int idCompte)
        {
            try
            {
                var existingCompteBancaire = await _databaseContext.compteBancaire.FindAsync(idCompte);
                if (existingCompteBancaire == null)
                {
                    return new List<EntityResponse> { new EntityResponse { success = false, message = "CompteBancaire non trouvé" } };
                }

                var transactions = await _databaseContext.transaction
                    .Where(t => t.idCompte == idCompte)
                    .OrderByDescending(t => t.dateHeure)
                    .Select(t => new Transaction
                    {
                        idTransaction = t.idTransaction,
                        idCompte = t.idCompte,
                        typeTransaction = t.typeTransaction,
                        montant = t.montant,
                        dateHeure = t.dateHeure,
                        statut = t.statut
                    })
                    .ToListAsync();

                if (transactions.Count == 0)
                {
                    return new List<EntityResponse> { new EntityResponse { success = false, message = "Aucune transaction trouvée!" } };
                }

                return transactions.Select(t => new EntityResponse { success = true, message = "Transactions trouvées!", transactionResponse = t }).ToList();
            }
            catch (Exception ex)
            {
                return new List<EntityResponse> { new EntityResponse { success = false, message = "Erreur lors de la recherche des transactions : " + ex.Message } };
            }
        }
    }
}
