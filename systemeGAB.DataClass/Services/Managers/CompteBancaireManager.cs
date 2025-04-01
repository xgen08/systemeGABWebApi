

namespace systemeGAB.DataClass.Services.Managers
{
    public class CompteBancaireManager : ICompteBancaireManager
    {
        private readonly DatabaseContext _databaseContext;

        public CompteBancaireManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<EntityResponse> AddCompte(AddCompteBancaireRequest request)
        {
            try
            {
                var existingClient = await _databaseContext.client.FindAsync(request.idClient);
                if (existingClient == null)
                {
                    return new EntityResponse { success = false, message = "Ce compte n'existe pas !" };
                }

                CompteBancaire compteBancaireToAdd = new CompteBancaire
                {
                    idClient = existingClient.idClient,
                    numeroCompte = request.numeroCompte,
                    solde = request.solde,
                    typeCompte = request.typeCompte,
                    dateOuverture = DateTime.Now
                };

                await _databaseContext.compteBancaire.AddAsync(compteBancaireToAdd);
                await _databaseContext.SaveChangesAsync();

                if(compteBancaireToAdd.idCompte <= 0)
                {
                    return new EntityResponse { success = false, message = "Erreur lors de l'ajout du compte" };
                }

                return new EntityResponse { success = true, message = "Compte ajouté avec succès", compteBancaireResponse = compteBancaireToAdd };

            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de l'ajout du client : " + ex.Message };
            }
        }

        public async Task<List<EntityResponse>> GetCompte()
        {
            try
            {
                var comptes = await _databaseContext.compteBancaire
                    .OrderByDescending(cb => cb.idCompte)
                    .Select(cb => new CompteBancaire
                    {
                        idCompte = cb.idCompte,
                        idClient = cb.idClient,
                        numeroCompte = cb.numeroCompte,
                        solde = cb.solde,
                        typeCompte = cb.typeCompte,
                        dateOuverture = cb.dateOuverture
                    })
                    .ToListAsync();

                if (comptes.Count == 0)
                {
                    return new List<EntityResponse> { new EntityResponse { success = false, message = "Aucun compte trouvé!" } };
                }

                return comptes.Select(c => new EntityResponse { success = true, message = "Compte trouvé!", compteBancaireResponse = c }).ToList();
            }
            catch (Exception ex)
            {
                return new List<EntityResponse> { new EntityResponse { success = false, message = "Erreur lors de la mise à jour : " + ex.Message } };
            }
        }

        public async Task<EntityResponse> GetCompteById(int idCompte)
        {
            try
            {
                var compte = await _databaseContext.compteBancaire
                    .Where(cb => cb.idCompte == idCompte)
                    .Select(cb => new CompteBancaire
                    {
                        idCompte = cb.idCompte,
                        idClient = cb.idClient,
                        numeroCompte = cb.numeroCompte,
                        solde = cb.solde,
                        typeCompte = cb.typeCompte,
                        dateOuverture = cb.dateOuverture
                    })
                    .FirstOrDefaultAsync();

                if (compte == null)
                {
                    return new EntityResponse { success = false, message = "Compte non trouvé!" };
                }

                return new EntityResponse { success = true, message = "Compte trouvé!", compteBancaireResponse = compte };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la recherche du client : " + ex.Message };
            }
        }

        public async Task<EntityResponse> UpdateCompte(UpdateCompteBancaireRequest request, int idCompte)
        {
            try
            {
                var existingCompte = await _databaseContext.compteBancaire.FindAsync(idCompte);
                if (existingCompte == null)
                {
                    return new EntityResponse { success = false, message = "Compte non trouvé!" };
                }

                existingCompte.numeroCompte = request.numeroCompte;
                existingCompte.solde = request.solde;
                existingCompte.typeCompte = request.typeCompte;

                if (_databaseContext.Entry(existingCompte).State == EntityState.Unchanged)
                {
                    return new EntityResponse { success = true, message = "Aucune modification détectée." };
                }

                await _databaseContext.SaveChangesAsync();

                return new EntityResponse { success = true, message = "Compte mis à jour avec succès!", compteBancaireResponse = existingCompte };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la mise à jour du compte : " + ex.Message };
            }
        }
    }
}
