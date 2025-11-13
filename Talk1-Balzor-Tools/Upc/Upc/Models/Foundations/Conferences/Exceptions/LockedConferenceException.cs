using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class LockedConferenceException : Xeption
    {
        public LockedConferenceException(Exception innerException)
            : base(message: "Locked conference record exception, please try again later", innerException)
        {
        }
    }
}