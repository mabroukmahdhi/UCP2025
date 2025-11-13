using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class ConferenceValidationException : Xeption
    {
        public ConferenceValidationException(Xeption innerException)
            : base(message: "Conference validation errors occurred, please try again.",
                  innerException)
        { }
    }
}