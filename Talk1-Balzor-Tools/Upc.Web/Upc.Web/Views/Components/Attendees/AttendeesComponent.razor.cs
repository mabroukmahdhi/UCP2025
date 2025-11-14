// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

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

        protected sealed class AttendeeView
        {
            public AttendeeView(
                string fullName,
                string company,
                string role,
                string email,
                string tag,
                string location)
            {
                this.FullName = fullName;
                this.Company = company;
                this.Role = role;
                this.Email = email;
                this.Tag = tag;
                this.Location = location;

                this.Initials = BuildInitials(fullName);
            }

            public string FullName { get; }

            public string Company { get; }

            public string Role { get; }

            public string Email { get; }

            public string Tag { get; }

            public string Location { get; }

            public string Initials { get; }

            private static string BuildInitials(string fullName)
            {
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    return "?";
                }

                string[] parts = fullName
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (parts.Length == 0)
                {
                    return "?";
                }

                if (parts.Length == 1)
                {
                    return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpperInvariant();
                }

                char first = parts[0][0];
                char last = parts[parts.Length - 1][0];

                return $"{char.ToUpperInvariant(first)}{char.ToUpperInvariant(last)}";
            }
        }
    }
}
