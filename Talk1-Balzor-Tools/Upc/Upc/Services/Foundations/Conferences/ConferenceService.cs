// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoInject.Attributes.TransientAttributes;
using Upc.Brokers.DateTimes;
using Upc.Brokers.Loggings;
using Upc.Brokers.Storages;
using Upc.Models.Foundations.Conferences;

namespace Upc.Services.Foundations.Conferences
{
    [Transient(typeof(IConferenceService))]
    public partial class ConferenceService : IConferenceService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ConferenceService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Conference> AddConferenceAsync(Conference conference) =>
            TryCatch(async () =>
            {
                ValidateConferenceOnAdd(conference);

                return await this.storageBroker.InsertConferenceAsync(conference);
            });

        public IQueryable<Conference> RetrieveAllConferences() =>
            TryCatch(() => this.storageBroker.SelectAllConferences());

        public ValueTask<Conference> RetrieveConferenceByIdAsync(Guid conferenceId) =>
            TryCatch(async () =>
            {
                ValidateConferenceId(conferenceId);

                Conference maybeConference = await this.storageBroker
                    .SelectConferenceByIdAsync(conferenceId);

                ValidateStorageConference(maybeConference, conferenceId);

                return maybeConference;
            });

        public ValueTask<Conference> ModifyConferenceAsync(Conference conference) =>
            TryCatch(async () =>
            {
                ValidateConferenceOnModify(conference);

                Conference maybeConference =
                    await this.storageBroker.SelectConferenceByIdAsync(conference.Id);

                ValidateStorageConference(maybeConference, conference.Id);

                return await this.storageBroker.UpdateConferenceAsync(conference);
            });

        public ValueTask<Conference> RemoveConferenceByIdAsync(Guid conferenceId) =>
            TryCatch(async () =>
            {
                ValidateConferenceId(conferenceId);

                Conference maybeConference = await this.storageBroker
                    .SelectConferenceByIdAsync(conferenceId);

                ValidateStorageConference(maybeConference, conferenceId);

                return await this.storageBroker.DeleteConferenceAsync(maybeConference);
            });
    }
}