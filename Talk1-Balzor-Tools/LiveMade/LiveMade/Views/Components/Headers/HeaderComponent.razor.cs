using Microsoft.AspNetCore.Components;

namespace LiveMade.Views.Components.Headers
{
    public partial class HeaderComponent : ComponentBase
    {
        [Parameter]
        public string ConferenceName { get; set; } = "Conference";

        [Parameter]
        public string Subtitle { get; set; } = "Conference Survey Platform";

        [Parameter]
        public bool IsLive { get; set; }

        [Parameter]
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
