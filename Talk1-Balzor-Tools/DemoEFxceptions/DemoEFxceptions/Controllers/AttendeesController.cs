using System.Linq;
using System.Threading.Tasks;
using DemoEFxceptions.Brokers.Storages;
using DemoEFxceptions.Models.Foundations.Attendees;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace DemoEFxceptions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : RESTFulController
    {
        private readonly IStorageBroker storageBroker;

        public AttendeesController(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        [HttpPost]
        public async ValueTask<ActionResult<Attendee>> PostAttendeeAsync(Attendee attendee)
        {
            Attendee addedAttendee =
                    await this.storageBroker.InsertAttendeeAsync(attendee);

            return Created(addedAttendee);
        }

        [HttpGet]
        public ActionResult<IQueryable<Attendee>> GetAllAttendees()
        {
            IQueryable<Attendee> retrievedAttendees =
                    this.storageBroker.SelectAllAttendees();

            return Ok(retrievedAttendees);
        }
    }
}