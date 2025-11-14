using System;
using System.Collections;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class InvalidAttendeeException : Xeption
    {
        public InvalidAttendeeException()
            : base(message: "Invalid Attendee. Correct the errors and try again.")
        { }

        public InvalidAttendeeException(Exception innerException, IDictionary data)
            : base(message: "Invalid Attendee error occurred. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
