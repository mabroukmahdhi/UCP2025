using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Attendees;

namespace Upc.Web.Services.Foundations.Attendees
{
    public interface IAttendeeService
    {
        ValueTask<Attendee> AddAttendeeAsync(Attendee attendee);
        ValueTask<List<Attendee>> RetrieveAllAttendeesAsync();
        ValueTask<Attendee> RetrieveAttendeeByIdAsync(Guid attendeeId);
        ValueTask<Attendee> ModifyAttendeeAsync(Attendee attendee);
        ValueTask<Attendee> RemoveAttendeeByIdAsync(Guid attendeeId);
    }
}