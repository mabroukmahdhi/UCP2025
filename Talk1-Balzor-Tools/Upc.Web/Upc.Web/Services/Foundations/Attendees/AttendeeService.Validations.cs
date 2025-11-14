// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using Upc.Web.Models.Attendees.Exceptions;
using Upc.Web.Models.Foundations.Attendees;

namespace Upc.Web.Services.Foundations.Attendees  // Attendee  attendee
{
    public partial class AttendeeService
    {
        private void ValidateAttendeeOnAdd(Attendee attendee)
        {
            ValidateAttendeeIsNotNull(attendee);

            Validate(
                (Rule: IsInvalid(attendee.Id), Parameter: nameof(Attendee.Id)),
                (Rule: IsInvalid(attendee.CreatedDate), Parameter: nameof(Attendee.CreatedDate)),
                (Rule: IsInvalid(attendee.UpdatedDate), Parameter: nameof(Attendee.UpdatedDate)));
        }

        private void ValidateAttendeeOnUpdate(Attendee attendee)
        {
            ValidateAttendeeIsNotNull(attendee);

            Validate(
                (Rule: IsInvalid(attendee.Id), Parameter: nameof(Attendee.Id)),
                (Rule: IsInvalid(attendee.CreatedDate), Parameter: nameof(Attendee.CreatedDate)),
                (Rule: IsInvalid(attendee.UpdatedDate), Parameter: nameof(Attendee.UpdatedDate)));
        }

        private static void ValidateAttendeeIsNotNull(Attendee attendee)
        {
            if (attendee is null)
            {
                throw new NullAttendeeException();
            }
        }

        public static void ValidateAttendeeId(Guid attendeeId) =>
            Validate((Rule: IsInvalid(attendeeId), Parameter: nameof(Attendee.Id)));

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAttendeeException = new InvalidAttendeeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAttendeeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAttendeeException.ThrowIfContainsErrors();
        }
    }
}