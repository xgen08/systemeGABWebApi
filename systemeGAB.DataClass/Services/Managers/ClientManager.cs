namespace systemeGAB.DataClass.Services.Managers
{
    public class ClientManager : IClientManager
    {
        private readonly DatabaseContext _databaseContext;

        public ClientManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<EntityResponse> AddClient(AddClientRequest request)
        {
            try
            {
                Client clientToAdd = new Client
                {
                    nomClient = request.nomClient,
                    prenomClient = request.prenomClient,
                    adresseClient = request.adresseClient,
                    emailClient = request.emailClient,
                    telephoneClient = request.telephoneClient
                };

                await _databaseContext.client.AddAsync(clientToAdd);
                await _databaseContext.SaveChangesAsync();

                if (clientToAdd.idClient <= 0)
                {
                    return new EntityResponse { success = false, message = "Erreur lors de l'ajout du client!" };
                }

                return new EntityResponse { success = true, message = "Client ajouté avec succès!", clientResponse = clientToAdd };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de l'ajout du client : " + ex.Message };
            }
        }

        public async Task<EntityResponse> GetClientById(int idClient)
        {
            try
            {
                var client = await _databaseContext.client
                    .Where(c => c.idClient == idClient)
                    .Select(c => new Client
                    {
                        idClient = c.idClient,
                        nomClient = c.nomClient,
                        prenomClient = c.prenomClient,
                        adresseClient = c.adresseClient,
                        emailClient = c.emailClient,
                        telephoneClient = c.telephoneClient
                    })
                    .FirstOrDefaultAsync();

                if (client == null)
                {
                    return new EntityResponse { success = false, message = "Client non trouvé!" };
                }

                return new EntityResponse { success = true, message = "Client trouvé!", clientResponse = client };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la recherche du client : " + ex.Message };
            }
        }


        public async Task<List<EntityResponse>> GetClients()
        {
            try
            {
                var clients = await _databaseContext.client
                    .OrderByDescending(c => c.idClient)
                    .Select(c => new Client
                    {
                        idClient = c.idClient,
                        nomClient = c.nomClient,
                        prenomClient = c.prenomClient,
                        adresseClient = c.adresseClient,
                        emailClient = c.emailClient,
                        telephoneClient = c.telephoneClient
                    })
                    .ToListAsync();

                if (clients.Count == 0)
                {
                    return new List<EntityResponse> { new EntityResponse { success = false, message = "Aucun client trouvé!" } };
                }

                return clients.Select(c => new EntityResponse { success = true, message = "Client trouvé!", clientResponse = c }).ToList();
            }
            catch (Exception ex)
            {
                return new List<EntityResponse> { new EntityResponse { success = false, message = "Erreur lors de la mise à jour : " + ex.Message } };
            }
        }

        public async Task<EntityResponse> UpdateClient(UpdateClientRequest request, int idClient)
        {
            try
            {
                var existingClient = await _databaseContext.client.FindAsync(idClient);
                if (existingClient == null)
                {
                    return new EntityResponse { success = false, message = "Ce client n'a pas été trouvé!" };
                }

                existingClient.nomClient = request.nomClient ?? existingClient.nomClient;
                existingClient.prenomClient = request.prenomClient ?? existingClient.prenomClient;
                existingClient.adresseClient = request.adresseClient ?? existingClient.adresseClient;
                existingClient.emailClient = request.emailClient ?? existingClient.emailClient;
                existingClient.telephoneClient = request.telephoneClient ?? existingClient.telephoneClient;

                if (_databaseContext.Entry(existingClient).State == EntityState.Unchanged)
                {
                    return new EntityResponse { success = true, message = "Aucune modification détectée." };
                }

                await _databaseContext.SaveChangesAsync();

                return new EntityResponse { success = true, message = "Client modifié avec succès!", clientResponse = existingClient };
            }
            catch (Exception ex)
            {
                return new EntityResponse { success = false, message = "Erreur lors de la mise à jour : " + ex.Message };
            }
        }

    }
}
