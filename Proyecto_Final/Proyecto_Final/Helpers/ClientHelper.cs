using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data;
using Proyecto_Final.Data.Entidades;
using Proyecto_Final.Models;

namespace Proyecto_Final.Helpers
{
    public class ClientHelper : IClientHelper
    {
        private readonly DataContext _context;

        public ClientHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<Client> AddClientAsync(ClientViewModel model)
        {
            Client client = new Client
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Email = model.Email,
                Description = model.Description               
            };           
            
            return client;
        }

        public async Task<Client> GetClientAsync(string phone)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.PhoneNumber == phone);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
