using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Data.Entidades;

namespace Proyecto_Final.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Service> Servicios { get; set; }


    }
}
