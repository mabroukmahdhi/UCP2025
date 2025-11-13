// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Upc.Models.Foundations.Conferences;

namespace Upc.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Conference> InsertConferenceAsync(Conference conference);
        IQueryable<Conference> SelectAllConferences();
        ValueTask<Conference> SelectConferenceByIdAsync(Guid conferenceId);
        ValueTask<Conference> UpdateConferenceAsync(Conference conference);
        ValueTask<Conference> DeleteConferenceAsync(Conference conference);
    }
}
