using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Mono.TextTemplating;
using Proyecto_Final.Data.Entidades;
using System.Diagnostics.Metrics;

namespace Proyecto_Final.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Service> Servicios { get; set; }
        public DbSet<Client> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Service>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(c => c.PhoneNumber).IsUnique();     //Indices Compuestos
           
        }


    }
}
