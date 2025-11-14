using System;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class AttendeeValidationException : Xeption
    {
        public AttendeeValidationException(Exception innerException)
            : base(message: "Attendee validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
