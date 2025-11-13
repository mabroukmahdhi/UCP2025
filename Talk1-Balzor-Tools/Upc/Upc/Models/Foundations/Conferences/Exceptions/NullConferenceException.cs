using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class NullConferenceException : Xeption
    {
        public NullConferenceException()
            : base(message: "Conference is null.")
        { }
    }
}