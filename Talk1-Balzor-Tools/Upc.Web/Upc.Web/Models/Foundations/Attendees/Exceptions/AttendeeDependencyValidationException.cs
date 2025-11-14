using Xeptions;

namespace Upc.Web.Models.Attendees.Exceptions
{
    public class AttendeeDependencyValidationException : Xeption
    {
        public AttendeeDependencyValidationException(Xeption innerException)
            : base(message: "Attendee dependency validation error occurred, please try again.",
                  innerException)
        { }
    }
}
