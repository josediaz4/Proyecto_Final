using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Models;

namespace Proyecto_Final.Helpers
{
    public interface IClientHelper
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(string phone);

        Task<Client> AddClientAsync(ClientViewModel model);

    }
}
