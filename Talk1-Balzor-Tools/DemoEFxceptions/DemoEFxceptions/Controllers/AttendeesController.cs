using System.Linq;
using System.Threading.Tasks;
using DemoEFxceptions.Brokers.Storages;
using DemoEFxceptions.Models.Foundations.Attendees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RESTFulSense.Controllers;

namespace DemoEFxceptions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : RESTFulController
    {
        private readonly IStorageBroker defaultStorageBroker;
        private readonly IStorageBroker meaningfulStorageBroker;

        public AttendeesController(
            [FromKeyedServices("MSB")] IStorageBroker meaningfulStorageBroker,
            [FromKeyedServices("DSB")] IStorageBroker defaultStorageBroker)
        {
            this.meaningfulStorageBroker = meaningfulStorageBroker;
            this.defaultStorageBroker = defaultStorageBroker;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Attendee>> PostAttendeeAsync(Attendee attendee)
        {
            Attendee addedAttendee =
                    await this.meaningfulStorageBroker.InsertAttendeeAsync(attendee);

            return Created(addedAttendee);
        }

        [HttpPost("default")]
        public async ValueTask<ActionResult<Attendee>> DefaultPostAttendeeAsync(Attendee attendee)
        {
            Attendee addedAttendee =
                    await this.defaultStorageBroker.InsertAttendeeAsync(attendee);

            return Created(addedAttendee);
        }

        [HttpGet]
        public ActionResult<IQueryable<Attendee>> GetAllAttendees()
        {
            IQueryable<Attendee> retrievedAttendees =
                    this.meaningfulStorageBroker.SelectAllAttendees();

            return Ok(retrievedAttendees);
        }
    }
}