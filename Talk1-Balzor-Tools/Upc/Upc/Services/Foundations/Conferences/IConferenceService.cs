// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Upc.Models.Foundations.Conferences;

namespace Upc.Services.Foundations.Conferences
{
    public interface IConferenceService
    {
        ValueTask<Conference> AddConferenceAsync(Conference conference);
        IQueryable<Conference> RetrieveAllConferences();
        ValueTask<Conference> RetrieveConferenceByIdAsync(Guid conferenceId);
        ValueTask<Conference> ModifyConferenceAsync(Conference conference);
        ValueTask<Conference> RemoveConferenceByIdAsync(Guid conferenceId);
    }
}