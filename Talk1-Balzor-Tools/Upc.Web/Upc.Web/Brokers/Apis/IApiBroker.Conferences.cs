// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Conferences;

namespace Upc.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Conference> PostConferenceAsync(Conference conference);
        ValueTask<List<Conference>> GetAllConferencesAsync();
        ValueTask<Conference> GetConferenceByIdAsync(Guid conferenceId);
        ValueTask<Conference> PutConferenceAsync(Conference conference);
        ValueTask<Conference> DeleteConferenceByIdAsync(Guid conferenceId);
    }
}