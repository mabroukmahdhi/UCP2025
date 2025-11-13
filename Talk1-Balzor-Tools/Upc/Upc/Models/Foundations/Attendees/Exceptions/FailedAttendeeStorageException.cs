using System;
using Xeptions;

namespace Upc.Models.Foundations.Attendees.Exceptions
{
    public class FailedAttendeeStorageException : Xeption
    {
        public FailedAttendeeStorageException(Exception innerException)
            : base(message: "Failed attendee storage error occurred, contact support.", innerException)
        { }
    }
}