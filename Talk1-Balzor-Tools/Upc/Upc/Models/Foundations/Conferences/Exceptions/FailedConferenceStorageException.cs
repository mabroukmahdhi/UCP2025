using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class FailedConferenceStorageException : Xeption
    {
        public FailedConferenceStorageException(Exception innerException)
            : base(message: "Failed conference storage error occurred, contact support.", innerException)
        { }
    }
}