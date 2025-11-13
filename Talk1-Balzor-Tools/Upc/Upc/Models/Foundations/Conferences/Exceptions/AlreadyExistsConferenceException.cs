using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class AlreadyExistsConferenceException : Xeption
    {
        public AlreadyExistsConferenceException(Exception innerException)
            : base(message: "Conference with the same Id already exists.", innerException)
        { }
    }
}