// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace Upc.Web.Views.Components.Headers
{
    public partial class HeaderComponent : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; } 

        protected void NavigateToWelcome()
        {
            this.NavigationManager.NavigateTo("/");
        }

        protected void NavigateToAttendees()
        {
            this.NavigationManager.NavigateTo("/attendees");
        }

        protected void NavigateToConferences()
        {
            this.NavigationManager.NavigateTo("/conferences");
        }
    }
}