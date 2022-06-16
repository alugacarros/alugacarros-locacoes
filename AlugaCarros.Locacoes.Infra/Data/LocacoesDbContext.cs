using AlugaCarros.Locacoes.Domain.Entities;
using AlugaCarros.Locacoes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlugaCarros.Locacoes.Infra.Data
{
    public class LocacoesDbContext : DbContext, IUnitOfWork
    {
        public LocacoesDbContext(DbContextOptions<LocacoesDbContext> options) : base(options)
        {

        }

        public DbSet<Location> Locations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(LocacoesDbContext).Assembly);

            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
