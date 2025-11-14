// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Upc.Web.Models.Conferences.Exceptions;
using Upc.Web.Models.Foundations.Conferences;
using Xeptions;

namespace Upc.Web.Services.Foundations.Conferences
{
    public partial class ConferenceService
    {
        private delegate ValueTask<Conference> ReturningConferenceFunction();
        private delegate ValueTask<List<Conference>> ReturningConferencesFunction();

        private async ValueTask<Conference> TryCatch(ReturningConferenceFunction returningConferenceFunction)
        {
            try
            {
                return await returningConferenceFunction();
            }
            catch (NullConferenceException nullConferenceException)
            {
                throw CreateAndLogValidationException(nullConferenceException);
            }
            catch (InvalidConferenceException invalidConferenceException)
            {
                throw CreateAndLogValidationException(invalidConferenceException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundConferenceException =
                    new NotFoundConferenceException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundConferenceException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidConferenceException =
                    new InvalidConferenceException(
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidConferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidConferenceException =
                    new InvalidConferenceException(
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidConferenceException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedConferenceException =
                    new LockedConferenceException(httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedConferenceException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedConferenceDependencyException);
            }
            catch (Exception exception)
            {
                var failedConferenceServiceException =
                    new FailedConferenceServiceException(exception);

                throw CreateAndLogConferenceServiceException(failedConferenceServiceException);
            }
        }

        private async ValueTask<List<Conference>> TryCatch(ReturningConferencesFunction returningConferencesFunction)
        {
            try
            {
                return await returningConferencesFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedConferenceDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedConferenceDependencyException =
                    new FailedConferenceDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedConferenceDependencyException);
            }
            catch (Exception exception)
            {
                var failedConferenceServiceException =
                    new FailedConferenceServiceException(exception);

                throw CreateAndLogConferenceServiceException(failedConferenceServiceException);
            }
        }

        private ConferenceValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var conferenceValidationException =
                new ConferenceValidationException(exception);

            this.loggingBroker.LogError(conferenceValidationException);

            return conferenceValidationException;
        }

        private ConferenceDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var conferenceDependencyException =
                new ConferenceDependencyException(exception);

            this.loggingBroker.LogCritical(conferenceDependencyException);

            return conferenceDependencyException;
        }

        private ConferenceDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var conferenceDependencyValidationException =
                new ConferenceDependencyValidationException(exception);

            this.loggingBroker.LogError(conferenceDependencyValidationException);

            return conferenceDependencyValidationException;
        }

        private ConferenceDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var conferenceDependencyException =
                new ConferenceDependencyException(exception);

            this.loggingBroker.LogError(conferenceDependencyException);

            return conferenceDependencyException;
        }

        private ConferenceServiceException CreateAndLogConferenceServiceException(Xeption exception)
        {
            var conferenceServiceException =
                new ConferenceServiceException(exception);

            this.loggingBroker.LogError(conferenceServiceException);

            return conferenceServiceException;
        }
    }
}