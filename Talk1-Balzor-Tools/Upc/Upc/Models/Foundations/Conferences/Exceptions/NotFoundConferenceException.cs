using System;
using Xeptions;

namespace Upc.Models.Foundations.Conferences.Exceptions
{
    public class NotFoundConferenceException : Xeption
    {
        public NotFoundConferenceException(Guid conferenceId)
            : base(message: $"Couldn't find conference with conferenceId: {conferenceId}.")
        { }
    }
}