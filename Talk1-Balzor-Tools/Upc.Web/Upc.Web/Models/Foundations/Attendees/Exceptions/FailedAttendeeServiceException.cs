using System;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class FailedAttendeeServiceException : Xeption
    {
        public FailedAttendeeServiceException(Exception innerException)
            : base(message: "Failed Attendee service error occurred, contact support.",
                  innerException)
        { }
    }
}
