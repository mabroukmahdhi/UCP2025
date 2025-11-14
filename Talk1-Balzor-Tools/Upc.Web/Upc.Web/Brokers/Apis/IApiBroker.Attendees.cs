// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Attendees;

namespace Upc.Web.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Attendee> PostAttendeeAsync(Attendee attendee);
        ValueTask<List<Attendee>> GetAllAttendeesAsync();
        ValueTask<Attendee> GetAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> PutAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> DeleteAttendeeByIdAsync(Guid attendeeId);
    }
}