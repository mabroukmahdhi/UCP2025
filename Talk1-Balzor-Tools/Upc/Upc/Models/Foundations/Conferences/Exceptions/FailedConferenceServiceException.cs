using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class FailedConferenceServiceException : Xeption
    {
        public FailedConferenceServiceException(Exception innerException)
            : base(message: "Failed conference service occurred, please contact support", innerException)
        { }
    }
}