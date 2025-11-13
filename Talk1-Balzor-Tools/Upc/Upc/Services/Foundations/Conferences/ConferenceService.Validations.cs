// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using Upc.Models.Foundations.Conferences;
using Upc.Models.Foundations.Conferences.Exceptions;

namespace Upc.Services.Foundations.Conferences
{
    public partial class ConferenceService
    {
        private void ValidateConferenceOnAdd(Conference conference)
        {
            ValidateConferenceIsNotNull(conference);

            Validate(
                (Rule: IsInvalid(conference.Id), Parameter: nameof(Conference.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(conference.CreatedDate), Parameter: nameof(Conference.CreatedDate)), 
                (Rule: IsInvalid(conference.UpdatedDate), Parameter: nameof(Conference.UpdatedDate)), 

                (Rule: IsNotSame(
                    firstDate: conference.UpdatedDate,
                    secondDate: conference.CreatedDate,
                    secondDateName: nameof(Conference.CreatedDate)),
                Parameter: nameof(Conference.UpdatedDate)),

                (Rule: IsNotRecent(conference.CreatedDate), Parameter: nameof(Conference.CreatedDate)));
        }

        private void ValidateConferenceOnModify(Conference conference)
        {
            ValidateConferenceIsNotNull(conference);

            Validate(
                (Rule: IsInvalid(conference.Id), Parameter: nameof(Conference.Id)),

                // TODO: Add any other required validation rules

                (Rule: IsInvalid(conference.CreatedDate), Parameter: nameof(Conference.CreatedDate)), 
                (Rule: IsInvalid(conference.UpdatedDate), Parameter: nameof(Conference.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: conference.UpdatedDate,
                    secondDate: conference.CreatedDate,
                    secondDateName: nameof(Conference.CreatedDate)),
                Parameter: nameof(Conference.UpdatedDate)),

                (Rule: IsNotRecent(conference.UpdatedDate), Parameter: nameof(conference.UpdatedDate)));
        }

        public void ValidateConferenceId(Guid conferenceId) =>
            Validate((Rule: IsInvalid(conferenceId), Parameter: nameof(Conference.Id)));

        private static void ValidateStorageConference(Conference maybeConference, Guid conferenceId)
        {
            if (maybeConference is null)
            {
                throw new NotFoundConferenceException(conferenceId);
            }
        }

        private static void ValidateConferenceIsNotNull(Conference conference)
        {
            if (conference is null)
            {
                throw new NullConferenceException();
            }
        } 

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTimeOffset();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

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