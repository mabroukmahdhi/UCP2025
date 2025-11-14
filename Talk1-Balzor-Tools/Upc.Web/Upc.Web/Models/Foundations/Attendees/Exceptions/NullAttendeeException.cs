using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class NullAttendeeException : Xeption
    {
        public NullAttendeeException()
            : base(message: "The Attendee is null.")
        { }
    }
}
