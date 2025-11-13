using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class ConferenceDependencyValidationException : Xeption
    {
        public ConferenceDependencyValidationException(Xeption innerException)
            : base(message: "Conference dependency validation occurred, please try again.", innerException)
        { }
    }
}