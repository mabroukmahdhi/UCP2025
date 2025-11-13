using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class ConferenceDependencyException : Xeption
    {
        public ConferenceDependencyException(Xeption innerException) :
            base(message: "Conference dependency error occurred, contact support.", innerException)
        { }
    }
}