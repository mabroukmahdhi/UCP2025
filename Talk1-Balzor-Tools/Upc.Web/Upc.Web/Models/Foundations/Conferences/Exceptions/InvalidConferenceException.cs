using System;
using System.Collections;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class InvalidConferenceException : Xeption
    {
        public InvalidConferenceException()
            : base(message: "Invalid Conference. Correct the errors and try again.")
        { }

        public InvalidConferenceException(Exception innerException, IDictionary data)
            : base(message: "Invalid Conference error occurred. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
