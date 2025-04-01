namespace systemeGAB.DataClass.Services.Interfaces
{
    public interface IClientManager
    {
        Task<List<EntityResponse>> GetClients();
        Task<EntityResponse> GetClientById(int idClient);
        Task<EntityResponse> AddClient(AddClientRequest request);
        Task<EntityResponse> UpdateClient(UpdateClientRequest request, int idClient);
    }
}
