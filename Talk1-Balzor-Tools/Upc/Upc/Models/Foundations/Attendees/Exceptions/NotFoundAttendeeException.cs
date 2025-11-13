using System;
using Xeptions;

namespace Upc.Models.Foundations.Attendees.Exceptions
{
    public class NotFoundAttendeeException : Xeption
    {
        public NotFoundAttendeeException(Guid attendeeId)
            : base(message: $"Couldn't find attendee with attendeeId: {attendeeId}.")
        { }
    }
}