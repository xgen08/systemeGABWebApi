namespace systemeGAB.DataClass.Services.Interfaces
{
    public interface ICarteBancaireManager
    {
        Task<List<EntityResponse>> GetCarteBancaires();
        Task<EntityResponse> GetCarteBancaireById(int idCarte);
        Task<EntityResponse> AddCarteBancaire(AddCarteBancaireRequest request);
        Task<EntityResponse> UpdateCartebancaire(UpdateCarteBancaireRequest request, int idCarte);
    }
}
