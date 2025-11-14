// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Conferences;

namespace Upc.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string ConferencesRelativeUrl = "api/Conferences";

        public async ValueTask<Conference> PostConferenceAsync(Conference conference) =>
            await this.PostAsync(ConferencesRelativeUrl, conference);

        public async ValueTask<List<Conference>> GetAllConferencesAsync() =>
            await this.GetAsync<List<Conference>>(ConferencesRelativeUrl);

        public async ValueTask<Conference> GetConferenceByIdAsync(Guid conferenceId) =>
            await this.GetAsync<Conference>($"{ConferencesRelativeUrl}/{conferenceId}");

        public async ValueTask<Conference> PutConferenceAsync(Conference conference) =>
            await this.PutAsync(ConferencesRelativeUrl, conference);

        public async ValueTask<Conference> DeleteConferenceByIdAsync(Guid conferenceId) =>
            await this.DeleteAsync<Conference>($"{ConferencesRelativeUrl}/{conferenceId}");
    }
}