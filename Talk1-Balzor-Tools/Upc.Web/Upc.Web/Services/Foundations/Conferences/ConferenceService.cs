using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Brokers.DateTimes;
using Upc.Web.Brokers.Loggings;
using Upc.Web.Brokers.Apis;
using Upc.Web.Models.Foundations.Conferences;

namespace Upc.Web.Services.Foundations.Conferences
{
    public partial class ConferenceService : IConferenceService
    {
        private readonly IApiBroker apiBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ConferenceService(
            IApiBroker apiBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Conference> AddConferenceAsync(Conference conference) =>
            TryCatch(async () =>
            {
                ValidateConferenceOnAdd(conference);

                return await this.apiBroker.PostConferenceAsync(conference);
            });

        public ValueTask<List<Conference>> RetrieveAllConferencesAsync() =>
            TryCatch(async () => await this.apiBroker.GetAllConferencesAsync());

       public ValueTask<Conference> RetrieveConferenceByIdAsync(Guid conferenceId) =>
        TryCatch(async () =>
        {
            ValidateConferenceId(conferenceId);

            return await this.apiBroker.GetConferenceByIdAsync(conferenceId);
        });

        public ValueTask<Conference> ModifyConferenceAsync(Conference conference) =>
        TryCatch(async () =>
        {
            ValidateConferenceOnUpdate(conference);

            return await this.apiBroker.PutConferenceAsync(conference);
        });

        public ValueTask<Conference> RemoveConferenceByIdAsync(Guid conferenceId) =>
        TryCatch(async () =>
        {
            ValidateConferenceId(conferenceId);

            return await this.apiBroker.DeleteConferenceByIdAsync(conferenceId);
        });
    }
}