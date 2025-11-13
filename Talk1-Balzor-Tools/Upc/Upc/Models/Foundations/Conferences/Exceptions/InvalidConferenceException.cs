using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class InvalidConferenceException : Xeption
    {
        public InvalidConferenceException()
            : base(message: "Invalid conference. Please correct the errors and try again.")
        { }
    }
}