using System;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class ConferenceValidationException : Xeption
    {
        public ConferenceValidationException(Exception innerException)
            : base(message: "Conference validation error ocurred, please try again.",
                  innerException)
        { }
    }
}
