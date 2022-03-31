using ElcodeTestTask.Models;
using ElcodeTestTask.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace ElcodeTestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientStatus> ClientStatus { get; set; }
        public DbSet<ClientRequest> ClientRequest { get; set; }
        public DbSet<ClientRequestType> ClientRequestType { get; set; }
        public DbSet<RequestProviderType> RequestProviderType { get; set; }
    }
}