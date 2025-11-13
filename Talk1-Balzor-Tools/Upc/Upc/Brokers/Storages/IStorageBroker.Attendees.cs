// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Upc.Models.Foundations.Attendees;

namespace Upc.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Attendee> InsertAttendeeAsync(Attendee attendee);
        IQueryable<Attendee> SelectAllAttendees();
        ValueTask<Attendee> SelectAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> UpdateAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> DeleteAttendeeAsync(Attendee attendee);
    }
}
