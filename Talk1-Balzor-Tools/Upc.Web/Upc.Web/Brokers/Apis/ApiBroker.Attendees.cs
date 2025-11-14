// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Attendees;

namespace Upc.Web.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string AttendeesRelativeUrl = "api/Attendees";

        public async ValueTask<Attendee> PostAttendeeAsync(Attendee attendee) =>
            await this.PostAsync(AttendeesRelativeUrl, attendee);

        public async ValueTask<List<Attendee>> GetAllAttendeesAsync() =>
            await this.GetAsync<List<Attendee>>(AttendeesRelativeUrl);

        public async ValueTask<Attendee> GetAttendeeByIdAsync(Guid attendeeId) =>
            await this.GetAsync<Attendee>($"{AttendeesRelativeUrl}/{attendeeId}");

        public async ValueTask<Attendee> PutAttendeeAsync(Attendee attendee) =>
            await this.PutAsync(AttendeesRelativeUrl, attendee);

        public async ValueTask<Attendee> DeleteAttendeeByIdAsync(Guid attendeeId) =>
            await this.DeleteAsync<Attendee>($"{AttendeesRelativeUrl}/{attendeeId}");
    }
}