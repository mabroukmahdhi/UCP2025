using System;
using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class FailedAttendeeDependencyException : Xeption
    {
        public FailedAttendeeDependencyException(Exception innerException)
            : base(message: "Failed Attendee dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
