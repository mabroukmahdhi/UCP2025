using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class ConferenceDependencyValidationException : Xeption
    {
        public ConferenceDependencyValidationException(Xeption innerException)
            : base(message: "Conference dependency validation error occurred, please try again.",
                  innerException)
        { }
    }
}
