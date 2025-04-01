namespace systemeGAB.DataClass.Services.Interfaces
{
    public interface ITransactionManager
    {
        Task<List<EntityResponse>> GetTransactions(int idCompte);
        Task<EntityResponse> GetTransactionById(int idTransaction);
        Task<EntityResponse> AddTransaction(AddTransactionRequest request);
    }
}