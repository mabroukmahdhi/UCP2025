using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class NullConferenceException : Xeption
    {
        public NullConferenceException()
            : base(message: "The Conference is null.")
        { }
    }
}
