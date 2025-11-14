// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Upc.Web.Models.Views.Attendees;

namespace Upc.Web.Views.Components.Attendees
{
    public partial class AttendeesComponent : ComponentBase
    {
        private string searchText = string.Empty;

        protected string SearchText
        {
            get => this.searchText;
            set
            {
                if (this.searchText == value)
                {
                    return;
                }

                this.searchText = value ?? string.Empty;
                ApplyFilter();
            }
        }

        protected List<AttendeeView> Attendees { get; private set; } = new List<AttendeeView>();

        protected List<AttendeeView> FilteredAttendees { get; private set; } = new List<AttendeeView>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Sample data – replace with real data binding later
            this.Attendees = new List<AttendeeView>
            {
                new AttendeeView("Mabrouk Mahdhi", "CodeCampsis UG", "Principal Software Engineer", "mabrouk@example.com", "Speaker", "Darmstadt, DE"),
                new AttendeeView("Sarah Johnson", "CloudWorks", "DevOps Engineer", "sarah.johnson@example.com", "Attendee", "London, UK"),
                new AttendeeView("Alex Kim", "DataSphere", "Data Scientist", "alex.kim@example.com", "Attendee", "Seoul, KR"),
                new AttendeeView("Elena Rossi", "NextGen Apps", "Full-Stack Developer", "elena.rossi@example.com", "Speaker", "Milan, IT"),
                new AttendeeView("David Chen", "AI Horizons", "ML Engineer", "david.chen@example.com", "Organizer", "San Francisco, US")
            };

            this.FilteredAttendees = new List<AttendeeView>(this.Attendees);
        }

        private void ApplyFilter()
        {
            string term = this.searchText.Trim();

            if (string.IsNullOrWhiteSpace(term))
            {
                this.FilteredAttendees = new List<AttendeeView>(this.Attendees);
                StateHasChanged();
                return;
            }

            term = term.ToLowerInvariant();

            this.FilteredAttendees = this.Attendees
                .Where(attendee =>
                    Contains(attendee.FullName, term) ||
                    Contains(attendee.Company, term) ||
                    Contains(attendee.Role, term) ||
                    Contains(attendee.Email, term) ||
                    Contains(attendee.Tag, term) ||
                    Contains(attendee.Location, term))
                .ToList();

            StateHasChanged();
        }

        private static bool Contains(string value, string term)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.ToLowerInvariant().Contains(term);
        }
    }
}
