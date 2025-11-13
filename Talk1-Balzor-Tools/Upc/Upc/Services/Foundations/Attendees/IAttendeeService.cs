// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Upc.Models.Foundations.Attendees;

namespace Upc.Services.Foundations.Attendees
{
    public interface IAttendeeService
    {
        ValueTask<Attendee> AddAttendeeAsync(Attendee attendee);
        IQueryable<Attendee> RetrieveAllAttendees();
        ValueTask<Attendee> RetrieveAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> ModifyAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> RemoveAttendeeByIdAsync(Guid attendeeId);
    }
}