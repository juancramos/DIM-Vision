using DIM_Vision_Cross;
using DIM_Vision_Entities.Entities;
using DIM_Vision_Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DIM_Vision_Data.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Grammar> Grammars { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings[VisionConstants.DefaultConnectionString].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid gId = Guid.NewGuid();
            modelBuilder.Entity<Grammar>().HasData(
                new Grammar
                {
                    Id = gId,
                    Order = (int)decimal.Zero,
                    Name = "Vísion",
                    Value = "Vísion"
                }
            );
            Guid gcId = Guid.NewGuid();
            Guid gc1Id = Guid.NewGuid();
            modelBuilder.Entity<Choice>().HasData(
                new Choice
                {
                    Id = gcId,
                    GrammarId = gId,
                    Order = (int)decimal.Zero,
                    Name = "Vísion",
                    Value = "Vísion"
                },
                new Choice
                {
                    Id = gc1Id,
                    GrammarId = gId,
                    Order = (int)decimal.Zero,
                    Name = "que",
                    Value = "que"
                },
                new Choice
                {
                    ParentId = gc1Id,
                    GrammarId = gId,
                    Order = (int)decimal.One,
                    Name = "hay en pantalla",
                    Value = "GetScreenShot",
                    CboiceType = ChoiceType.SemanticResultValue
                },
                new Choice
                {
                    ParentId = gc1Id,
                    GrammarId = gId,
                    Order = (int)decimal.One,
                    Name = "estoy viendo",
                    Value = "GetScreenShot",
                    CboiceType = ChoiceType.SemanticResultValue
                },
                new Choice
                {
                    ParentId = gc1Id,
                    GrammarId = gId,
                    Order = (int)decimal.One,
                    Name = "esta abierto",
                    Value = "GetRunningApps",
                    CboiceType = ChoiceType.SemanticResultValue
                }
            );
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
