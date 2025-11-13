using System;
using Xeptions;

namespace Upc.Models.Foundations.Attendees.Exceptions
{
    public class InvalidAttendeeReferenceException : Xeption
    {
        public InvalidAttendeeReferenceException(Exception innerException)
            : base(message: "Invalid attendee reference error occurred.", innerException) { }
    }
}