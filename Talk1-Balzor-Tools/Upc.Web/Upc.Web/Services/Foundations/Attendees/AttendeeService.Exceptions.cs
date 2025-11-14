// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Upc.Web.Models.Attendees.Exceptions;
using Upc.Web.Models.Foundations.Attendees;
using Xeptions;

namespace Upc.Web.Services.Foundations.Attendees
{
    public partial class AttendeeService
    {
        private delegate ValueTask<Attendee> ReturningAttendeeFunction();
        private delegate ValueTask<List<Attendee>> ReturningAttendeesFunction();

        private async ValueTask<Attendee> TryCatch(ReturningAttendeeFunction returningAttendeeFunction)
        {
            try
            {
                return await returningAttendeeFunction();
            }
            catch (NullAttendeeException nullAttendeeException)
            {
                throw CreateAndLogValidationException(nullAttendeeException);
            }
            catch (InvalidAttendeeException invalidAttendeeException)
            {
                throw CreateAndLogValidationException(invalidAttendeeException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAttendeeException =
                    new NotFoundAttendeeException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundAttendeeException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAttendeeException =
                    new InvalidAttendeeException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidAttendeeException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidAttendeeException =
                    new InvalidAttendeeException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidAttendeeException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedAttendeeException =
                    new LockedAttendeeException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedAttendeeException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedAttendeeDependencyException);
            }
            catch (Exception exception)
            {
                var failedAttendeeServiceException =
                    new FailedAttendeeServiceException(exception);

                throw CreateAndLogAttendeeServiceException(failedAttendeeServiceException);
            }
        }

        private async ValueTask<List<Attendee>> TryCatch(ReturningAttendeesFunction returningAttendeesFunction)
        {
            try
            {
                return await returningAttendeesFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedAttendeeDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedAttendeeDependencyException =
                    new FailedAttendeeDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedAttendeeDependencyException);
            }
            catch (Exception exception)
            {
                var failedAttendeeServiceException =
                    new FailedAttendeeServiceException(exception);

                throw CreateAndLogAttendeeServiceException(failedAttendeeServiceException);
            }
        }

        private AttendeeValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var attendeeValidationException =
                new AttendeeValidationException(exception);

            this.loggingBroker.LogError(attendeeValidationException);

            return attendeeValidationException;
        }

        private AttendeeDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var attendeeDependencyException =
                new AttendeeDependencyException(exception);

            this.loggingBroker.LogCritical(attendeeDependencyException);

            return attendeeDependencyException;
        }

        private AttendeeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var attendeeDependencyValidationException =
                new AttendeeDependencyValidationException(exception);

            this.loggingBroker.LogError(attendeeDependencyValidationException);

            return attendeeDependencyValidationException;
        }

        private AttendeeDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var attendeeDependencyException =
                new AttendeeDependencyException(exception);

            this.loggingBroker.LogError(attendeeDependencyException);

            return attendeeDependencyException;
        }

        private AttendeeServiceException CreateAndLogAttendeeServiceException(Xeption exception)
        {
            var attendeeServiceException =
                new AttendeeServiceException(exception);

            this.loggingBroker.LogError(attendeeServiceException);

            return attendeeServiceException;
        }
    }
}