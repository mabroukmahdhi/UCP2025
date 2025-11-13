using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Upc.Models.Foundations.Conferences;
using Upc.Models.Foundations.Conferences.Exceptions;
using Upc.Services.Foundations.Conferences;

namespace Upc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferencesController : RESTFulController
    {
        private readonly IConferenceService conferenceService;

        public ConferencesController(IConferenceService conferenceService) =>
            this.conferenceService = conferenceService;

        [HttpPost]
        public async ValueTask<ActionResult<Conference>> PostConferenceAsync(Conference conference)
        {
            try
            {
                Conference addedConference =
                    await this.conferenceService.AddConferenceAsync(conference);

                return Created(addedConference);
            }
            catch (ConferenceValidationException conferenceValidationException)
            {
                return BadRequest(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceValidationException)
                when (conferenceValidationException.InnerException is InvalidConferenceReferenceException)
            {
                return FailedDependency(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceDependencyValidationException)
               when (conferenceDependencyValidationException.InnerException is AlreadyExistsConferenceException)
            {
                return Conflict(conferenceDependencyValidationException.InnerException);
            }
            catch (ConferenceDependencyException conferenceDependencyException)
            {
                return InternalServerError(conferenceDependencyException);
            }
            catch (ConferenceServiceException conferenceServiceException)
            {
                return InternalServerError(conferenceServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Conference>> GetAllConferences()
        {
            try
            {
                IQueryable<Conference> retrievedConferences =
                    this.conferenceService.RetrieveAllConferences();

                return Ok(retrievedConferences);
            }
            catch (ConferenceDependencyException conferenceDependencyException)
            {
                return InternalServerError(conferenceDependencyException);
            }
            catch (ConferenceServiceException conferenceServiceException)
            {
                return InternalServerError(conferenceServiceException);
            }
        }

        [HttpGet("{conferenceId}")]
        public async ValueTask<ActionResult<Conference>> GetConferenceByIdAsync(Guid conferenceId)
        {
            try
            {
                Conference conference = await this.conferenceService.RetrieveConferenceByIdAsync(conferenceId);

                return Ok(conference);
            }
            catch (ConferenceValidationException conferenceValidationException)
                when (conferenceValidationException.InnerException is NotFoundConferenceException)
            {
                return NotFound(conferenceValidationException.InnerException);
            }
            catch (ConferenceValidationException conferenceValidationException)
            {
                return BadRequest(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyException conferenceDependencyException)
            {
                return InternalServerError(conferenceDependencyException);
            }
            catch (ConferenceServiceException conferenceServiceException)
            {
                return InternalServerError(conferenceServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Conference>> PutConferenceAsync(Conference conference)
        {
            try
            {
                Conference modifiedConference =
                    await this.conferenceService.ModifyConferenceAsync(conference);

                return Ok(modifiedConference);
            }
            catch (ConferenceValidationException conferenceValidationException)
                when (conferenceValidationException.InnerException is NotFoundConferenceException)
            {
                return NotFound(conferenceValidationException.InnerException);
            }
            catch (ConferenceValidationException conferenceValidationException)
            {
                return BadRequest(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceValidationException)
                when (conferenceValidationException.InnerException is InvalidConferenceReferenceException)
            {
                return FailedDependency(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceDependencyValidationException)
               when (conferenceDependencyValidationException.InnerException is AlreadyExistsConferenceException)
            {
                return Conflict(conferenceDependencyValidationException.InnerException);
            }
            catch (ConferenceDependencyException conferenceDependencyException)
            {
                return InternalServerError(conferenceDependencyException);
            }
            catch (ConferenceServiceException conferenceServiceException)
            {
                return InternalServerError(conferenceServiceException);
            }
        }

        [HttpDelete("{conferenceId}")]
        public async ValueTask<ActionResult<Conference>> DeleteConferenceByIdAsync(Guid conferenceId)
        {
            try
            {
                Conference deletedConference =
                    await this.conferenceService.RemoveConferenceByIdAsync(conferenceId);

                return Ok(deletedConference);
            }
            catch (ConferenceValidationException conferenceValidationException)
                when (conferenceValidationException.InnerException is NotFoundConferenceException)
            {
                return NotFound(conferenceValidationException.InnerException);
            }
            catch (ConferenceValidationException conferenceValidationException)
            {
                return BadRequest(conferenceValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceDependencyValidationException)
                when (conferenceDependencyValidationException.InnerException is LockedConferenceException)
            {
                return Locked(conferenceDependencyValidationException.InnerException);
            }
            catch (ConferenceDependencyValidationException conferenceDependencyValidationException)
            {
                return BadRequest(conferenceDependencyValidationException);
            }
            catch (ConferenceDependencyException conferenceDependencyException)
            {
                return InternalServerError(conferenceDependencyException);
            }
            catch (ConferenceServiceException conferenceServiceException)
            {
                return InternalServerError(conferenceServiceException);
            }
        }
    }
}