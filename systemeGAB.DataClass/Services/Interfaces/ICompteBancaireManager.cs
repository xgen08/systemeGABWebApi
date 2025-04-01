namespace systemeGAB.DataClass.Services.Interfaces
{
    public interface ICompteBancaireManager
    {
        Task<List<EntityResponse>> GetCompte();
        Task<EntityResponse> GetCompteById(int idCompte);
        Task<EntityResponse> AddCompte(AddCompteBancaireRequest request);
        Task<EntityResponse> UpdateCompte(UpdateCompteBancaireRequest request, int idCompte);
    }
}