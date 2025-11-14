using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class ConferenceServiceException : Xeption
    {
        public ConferenceServiceException(Xeption innerException)
            : base(message: "Conference service error occurred, contact support.",
                  innerException)
        { }
    }
}
