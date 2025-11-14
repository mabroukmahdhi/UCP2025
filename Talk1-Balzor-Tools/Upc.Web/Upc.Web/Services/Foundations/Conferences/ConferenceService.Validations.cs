// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using Upc.Web.Models.Conferences.Exceptions;
using Upc.Web.Models.Foundations.Conferences;

namespace Upc.Web.Services.Foundations.Conferences  // Conference  conference
{
    public partial class ConferenceService
    {
        private void ValidateConferenceOnAdd(Conference conference)
        {
            ValidateConferenceIsNotNull(conference);

            Validate(
                (Rule: IsInvalid(conference.Id), Parameter: nameof(Conference.Id)),
                (Rule: IsInvalid(conference.CreatedDate), Parameter: nameof(Conference.CreatedDate)),
                (Rule: IsInvalid(conference.UpdatedDate), Parameter: nameof(Conference.UpdatedDate)));
        }

        private void ValidateConferenceOnUpdate(Conference conference)
        {
            ValidateConferenceIsNotNull(conference);

            Validate(
                (Rule: IsInvalid(conference.Id), Parameter: nameof(Conference.Id)),
                (Rule: IsInvalid(conference.CreatedDate), Parameter: nameof(Conference.CreatedDate)),
                (Rule: IsInvalid(conference.UpdatedDate), Parameter: nameof(Conference.UpdatedDate)));
        }

        private static void ValidateConferenceIsNotNull(Conference conference)
        {
            if (conference is null)
            {
                throw new NullConferenceException();
            }
        }

        public static void ValidateConferenceId(Guid conferenceId) =>
            Validate((Rule: IsInvalid(conferenceId), Parameter: nameof(Conference.Id)));

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
            var invalidConferenceException = new InvalidConferenceException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidConferenceException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidConferenceException.ThrowIfContainsErrors();
        }
    }
}