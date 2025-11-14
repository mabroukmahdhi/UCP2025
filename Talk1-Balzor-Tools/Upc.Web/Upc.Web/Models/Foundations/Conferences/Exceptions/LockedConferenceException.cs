using System;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class LockedConferenceException : Xeption
    {
        public LockedConferenceException(Exception innerException)
            : base(message: "Locked Conference error occurred, please try again later.",
                  innerException)
        { }
    }
}
