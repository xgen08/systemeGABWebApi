using systemeGAB.DataClass.EntityModels;

namespace systemeGAB.DataClass.Services.Managers
{
    public class CarteBancaireManager : ICarteBancaireManager
    {
        private readonly DatabaseContext _databaseContext;

        public CarteBancaireManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<EntityResponse> AddCarteBancaire(AddCarteBancaireRequest request)
        {
            try
            {
                var existingCompteBancaire = await _databaseContext.compteBancaire.FindAsync(request.idCompte);
                if (existingCompteBancaire == null)
                {
                    return new EntityResponse { success = false, message = "Compte Bancaire associe a la carte non trouvé!" };
                }

                CarteBancaire carteBancaireToAdd = new CarteBancaire
                {
                    idCompte = request.idCompte,
                    numeroCarte = request.numeroCarte,
                    dateExpiration = request.dateExpiration,
                    codePin = CodePinTool.HashCodePin(request.codePin!),
                    statut = request.statut
                };

                await _databaseContext.carteBancaire.AddAsync(carteBancaireToAdd);
                await _databaseContext.SaveChangesAsync();

                return new EntityResponse { success = true, message = "Carte Bancaire ajouté avec succès!", carteBancaireResponse = carteBancaireToAdd };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de l'ajout du client : " + ex.Message };
            }
        }

        public async Task<EntityResponse> GetCarteBancaireById(int idCarte)
        {
            try 
            {
                var carteBancaire = await _databaseContext.carteBancaire
                    .Where(c => c.idCarte == idCarte)
                    .Select(c => new CarteBancaire
                    {
                        idCarte = c.idCarte,
                        idCompte = c.idCompte,
                        numeroCarte = c.numeroCarte,
                        dateExpiration = c.dateExpiration,
                        statut = c.statut
                    })
                    .FirstOrDefaultAsync();

                if (carteBancaire == null)
                {
                    return new EntityResponse { success = false, message = "Carte Bancaire non trouvé!" };
                }

                return new EntityResponse { success = true, message = "Carte Bancaire trouvé avec succès!", carteBancaireResponse = carteBancaire };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la recherche du client : " + ex.Message };
            }
        }

        public async Task<List<EntityResponse>> GetCarteBancaires()
        {
            try
            {
                var carteBancaire = await _databaseContext.carteBancaire
                    .OrderByDescending(c => c.idCarte)
                    .Select(c => new CarteBancaire
                    {
                        idCarte = c.idCarte,
                        idCompte = c.idCompte,
                        numeroCarte = c.numeroCarte,
                        dateExpiration = c.dateExpiration,
                        statut = c.statut
                    })
                    .ToListAsync();

                if (carteBancaire == null)
                {
                    return new List<EntityResponse> { new EntityResponse { success = false, message = "Carte Bancaire non trouvé!" } };
                }
                return carteBancaire.Select(c => new EntityResponse { success = true, message = "Client trouvé!", carteBancaireResponse = c }).ToList();
            }
            catch (Exception ex)
            {
                return new List<EntityResponse> { new EntityResponse { success = false, message = "Erreur lors de la mise à jour : " + ex.Message } };
            }
        }

        public async Task<EntityResponse> UpdateCartebancaire(UpdateCarteBancaireRequest request, int idCarte)
        {
            try
            {
                var existingCarteBancaire = await _databaseContext.carteBancaire.FindAsync(idCarte);

                if (existingCarteBancaire == null)
                {
                    return new EntityResponse { success = false, message = "CarteBancaire not found" };
                }

                existingCarteBancaire.numeroCarte = request.numeroCarte;
                existingCarteBancaire.dateExpiration = request.dateExpiration;
                existingCarteBancaire.codePin = CodePinTool.HashCodePin(request.codePin!);
                existingCarteBancaire.statut = request.statut;

                if (_databaseContext.Entry(existingCarteBancaire).State == EntityState.Unchanged)
                {
                    return new EntityResponse { success = true, message = "Aucune modification détectée." };
                }

                await _databaseContext.SaveChangesAsync();

                return new EntityResponse { success = true, message = "Carte Bancaire mise à jour avec succès!", carteBancaireResponse = existingCarteBancaire };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la mise à jour : " + ex.Message };
            }
        }
    }
}
