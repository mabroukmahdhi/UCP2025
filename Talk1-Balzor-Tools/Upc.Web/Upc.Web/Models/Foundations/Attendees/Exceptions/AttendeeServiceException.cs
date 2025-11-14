using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class AttendeeServiceException : Xeption
    {
        public AttendeeServiceException(Xeption innerException)
            : base(message: "Attendee service error occurred, contact support.",
                  innerException)
        { }
    }
}
