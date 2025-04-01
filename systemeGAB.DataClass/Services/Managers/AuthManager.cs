
namespace systemeGAB.DataClass.Services.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly DatabaseContext _databaseContext;

        public AuthManager(IOptions<JwtSettings> jwtSettings, DatabaseContext databaseContext)
        {
            _jwtSettings = jwtSettings;
            _databaseContext = databaseContext;
        }

        public async Task<EntityResponse> ConnectCarte(ConnectCarteRequest request)
        {
            TokenModel result = null!;
            var existingCarteBancaire = await AuthentificationCarte(request.numeroCarte!, request.codePin!);
            if (existingCarteBancaire != null)
            {
                result = new TokenModel
                {
                    idCarte = existingCarteBancaire.idCarte,
                    numeroCarte = existingCarteBancaire.numeroCarte,
                    token = TokenTool.GenerateJwt(existingCarteBancaire, _jwtSettings.Value)
                };

                return new EntityResponse
                {
                    success = true, tokenResponse = result, message = "Success"
                };
            }
            return new EntityResponse
            {
                success = false, tokenResponse = result, message = "Numéro de carte ou code pin incorrecte"
            };
        }

        public async Task<CarteBancaire> AuthentificationCarte(string numeroCarte, string codePin)
        {
            CarteBancaire result = null!;
            var carteBancaire = await _databaseContext.carteBancaire.Where(c => c.numeroCarte == numeroCarte).FirstOrDefaultAsync();

            if(carteBancaire == null)
            {
                return result;
            }

            if (carteBancaire != null && carteBancaire.codePin == CodePinTool.HashCodePin(codePin!))
            {
                result = new CarteBancaire
                {
                    idCarte = carteBancaire.idCarte,
                    idCompte = carteBancaire.idCompte,
                    numeroCarte = carteBancaire.numeroCarte,
                    dateExpiration = carteBancaire.dateExpiration,
                    statut = carteBancaire.statut
                };
            }
            return result;
        }
    }
}
