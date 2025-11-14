using System;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class NotFoundAttendeeException : Xeption
    {
        public NotFoundAttendeeException(Exception innerException)
            : base(message: "Not found Attendee error occurred, please try again.",
                  innerException)
        { }
    }
}
