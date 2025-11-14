using System;
using Xeptions;

namespace Upc.Web.Models.Conferences.Exceptions
{
    public class NotFoundConferenceException : Xeption
    {
        public NotFoundConferenceException(Exception innerException)
            : base(message: "Not found Conference error occurred, please try again.",
                  innerException)
        { }
    }
}
