// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Upc.Models.Foundations.Conferences;
using Upc.Models.Foundations.Conferences.Exceptions;
using Xeptions;

namespace Upc.Services.Foundations.Conferences
{
    public partial class ConferenceService
    {
        private delegate ValueTask<Conference> ReturningConferenceFunction();
        private delegate IQueryable<Conference> ReturningConferencesFunction();

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
            catch (SqlException sqlException)
            {
                var failedConferenceStorageException =
                    new FailedConferenceStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedConferenceStorageException);
            }
            catch (NotFoundConferenceException notFoundConferenceException)
            {
                throw CreateAndLogValidationException(notFoundConferenceException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsConferenceException =
                    new AlreadyExistsConferenceException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsConferenceException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidConferenceReferenceException =
                    new InvalidConferenceReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidConferenceReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedConferenceException = new LockedConferenceException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedConferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedConferenceStorageException =
                    new FailedConferenceStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedConferenceStorageException);
            }
            catch (Exception exception)
            {
                var failedConferenceServiceException =
                    new FailedConferenceServiceException(exception);

                throw CreateAndLogServiceException(failedConferenceServiceException);
            }
        }

        private IQueryable<Conference> TryCatch(ReturningConferencesFunction returningConferencesFunction)
        {
            try
            {
                return returningConferencesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedConferenceStorageException =
                    new FailedConferenceStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedConferenceStorageException);
            }
            catch (Exception exception)
            {
                var failedConferenceServiceException =
                    new FailedConferenceServiceException(exception);

                throw CreateAndLogServiceException(failedConferenceServiceException);
            }
        }

        private ConferenceValidationException CreateAndLogValidationException(Xeption exception)
        {
            var conferenceValidationException =
                new ConferenceValidationException(exception);

            this.loggingBroker.LogError(conferenceValidationException);

            return conferenceValidationException;
        }

        private ConferenceDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var conferenceDependencyException = new ConferenceDependencyException(exception);
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

        private ConferenceDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var conferenceDependencyException = new ConferenceDependencyException(exception);
            this.loggingBroker.LogError(conferenceDependencyException);

            return conferenceDependencyException;
        }

        private ConferenceServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var conferenceServiceException = new ConferenceServiceException(exception);
            this.loggingBroker.LogError(conferenceServiceException);

            return conferenceServiceException;
        }
    }
}