using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Brokers.DateTimes;
using Upc.Web.Brokers.Loggings;
using Upc.Web.Brokers.Apis;
using Upc.Web.Models.Foundations.Attendees;

namespace Upc.Web.Services.Foundations.Attendees
{
    public partial class AttendeeService : IAttendeeService
    {
        private readonly IApiBroker apiBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public AttendeeService(
            IApiBroker apiBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Attendee> AddAttendeeAsync(Attendee attendee) =>
            TryCatch(async () =>
            {
                ValidateAttendeeOnAdd(attendee);

                return await this.apiBroker.PostAttendeeAsync(attendee);
            });

        public ValueTask<List<Attendee>> RetrieveAllAttendeesAsync() =>
            TryCatch(async () => await this.apiBroker.GetAllAttendeesAsync());

       public ValueTask<Attendee> RetrieveAttendeeByIdAsync(Guid attendeeId) =>
        TryCatch(async () =>
        {
            ValidateAttendeeId(attendeeId);

            return await this.apiBroker.GetAttendeeByIdAsync(attendeeId);
        });

        public ValueTask<Attendee> ModifyAttendeeAsync(Attendee attendee) =>
        TryCatch(async () =>
        {
            ValidateAttendeeOnUpdate(attendee);

            return await this.apiBroker.PutAttendeeAsync(attendee);
        });

        public ValueTask<Attendee> RemoveAttendeeByIdAsync(Guid attendeeId) =>
        TryCatch(async () =>
        {
            ValidateAttendeeId(attendeeId);

            return await this.apiBroker.DeleteAttendeeByIdAsync(attendeeId);
        });
    }
}