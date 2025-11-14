using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Upc.Web.Models.Foundations.Conferences;

namespace Upc.Web.Services.Foundations.Conferences
{
    public interface IConferenceService
    {
        ValueTask<Conference> AddConferenceAsync(Conference conference);
        ValueTask<List<Conference>> RetrieveAllConferencesAsync();
        ValueTask<Conference> RetrieveConferenceByIdAsync(Guid conferenceId);
        ValueTask<Conference> ModifyConferenceAsync(Conference conference);
        ValueTask<Conference> RemoveConferenceByIdAsync(Guid conferenceId);
    }
}