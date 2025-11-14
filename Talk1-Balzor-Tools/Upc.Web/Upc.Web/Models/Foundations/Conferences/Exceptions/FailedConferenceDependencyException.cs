using System;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class FailedConferenceDependencyException : Xeption
    {
        public FailedConferenceDependencyException(Exception innerException)
            : base(message: "Failed Conference dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
