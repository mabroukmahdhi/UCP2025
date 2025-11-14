using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class ConferenceDependencyException : Xeption
    {
        public ConferenceDependencyException(Xeption innerException)
            : base(message: "Conference dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
