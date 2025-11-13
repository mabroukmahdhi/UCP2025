using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class ConferenceServiceException : Xeption
    {
        public ConferenceServiceException(Exception innerException)
            : base(message: "Conference service error occurred, contact support.", innerException)
        { }
    }
}