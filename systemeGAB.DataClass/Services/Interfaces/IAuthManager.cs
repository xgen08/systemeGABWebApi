namespace systemeGAB.DataClass.Services.Interfaces
{
    public interface IAuthManager
    {
        Task<EntityResponse> ConnectCarte(ConnectCarteRequest request);
    }
}
