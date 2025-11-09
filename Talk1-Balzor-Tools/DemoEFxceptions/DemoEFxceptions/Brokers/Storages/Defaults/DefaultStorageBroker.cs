// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using AutoInject.Attributes.TransientAttributes;
using DemoEFxceptions.Brokers.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoEFxceptions.Brokers.DefaultStorages
{
    [Transient(typeof(IStorageBroker), "DSB")]
    public partial class DefaultStorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public DefaultStorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration
                .GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            var broker = new DefaultStorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return @object;
        }

        private IQueryable<T> SelectAll<T>() where T : class
        {
            var broker = new DefaultStorageBroker(this.configuration);

            return broker.Set<T>();
        }

        private async ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class
        {
            var broker = new DefaultStorageBroker(this.configuration);

            return await broker.FindAsync<T>(objectIds);
        }

        private async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var broker = new DefaultStorageBroker(this.configuration);
            broker.Entry(@object).State |= EntityState.Modified;
            await broker.SaveChangesAsync();

            return @object;
        }

        private async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var broker = new DefaultStorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Deleted;
            await broker.SaveChangesAsync();

            return @object;
        }
    }
}
