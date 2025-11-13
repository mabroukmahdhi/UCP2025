using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class InvalidConferenceReferenceException : Xeption
    {
        public InvalidConferenceReferenceException(Exception innerException)
            : base(message: "Invalid conference reference error occurred.", innerException) { }
    }
}