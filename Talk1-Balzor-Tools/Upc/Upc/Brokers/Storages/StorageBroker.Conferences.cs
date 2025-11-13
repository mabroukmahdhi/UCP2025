// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Upc.Models.Foundations.Conferences;

namespace Upc.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Conference> Conferences { get; set; }

        public async ValueTask<Conference> InsertConferenceAsync(Conference conference) =>
            await InsertAsync(conference);

        public IQueryable<Conference> SelectAllConferences() =>
            SelectAll<Conference>();

        public async ValueTask<Conference> SelectConferenceByIdAsync(Guid conferenceId) =>
            await SelectAsync<Conference>(conferenceId);

        public async ValueTask<Conference> UpdateConferenceAsync(Conference conference) =>
            await UpdateAsync(conference);

        public async ValueTask<Conference> DeleteConferenceAsync(Conference conference) =>
            await DeleteAsync(conference);

        internal void ConfigureConferences(EntityTypeBuilder<Conference> builder)
        {
            // TO DO: Configure the Conference entity
        }
    }
}
