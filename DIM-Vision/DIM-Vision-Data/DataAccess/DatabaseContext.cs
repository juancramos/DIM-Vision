using DIM_Vision_Cross;
using DIM_Vision_Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Threading.Tasks;

namespace DIM_Vision_Data.DataAccess
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Grammar> Grammars { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[VisionConstants.DefaultConnectionString].ConnectionString);
        }

        public virtual bool Commit()
        {
            return base.SaveChanges() > 0;
        }

        public virtual async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
