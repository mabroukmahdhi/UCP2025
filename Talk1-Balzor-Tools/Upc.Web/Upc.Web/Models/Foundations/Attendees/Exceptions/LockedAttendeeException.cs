using System;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class LockedAttendeeException : Xeption
    {
        public LockedAttendeeException(Exception innerException)
            : base(message: "Locked Attendee error occurred, please try again later.",
                  innerException)
        { }
    }
}
