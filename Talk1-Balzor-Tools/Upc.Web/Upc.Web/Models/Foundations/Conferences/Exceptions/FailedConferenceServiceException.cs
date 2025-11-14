using System;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class FailedConferenceServiceException : Xeption
    {
        public FailedConferenceServiceException(Exception innerException)
            : base(message: "Failed Conference service error occurred, contact support.",
                  innerException)
        { }
    }
}
