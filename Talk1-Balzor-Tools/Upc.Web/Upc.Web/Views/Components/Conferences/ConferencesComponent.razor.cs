// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Upc.Web.Models.Views.Conferences;

namespace Upc.Web.Views.Components.Conferences
{
    public partial class ConferencesComponent : ComponentBase
    {
        private string searchText = string.Empty;
        private string startDateText = string.Empty;
        private string typeFilterKey = string.Empty;
        private int currentPage = 1;

        private const int PageSize = 3;

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
                ApplyFilters();
            }
        }

        protected string StartDateText
        {
            get => this.startDateText;
            set
            {
                if (this.startDateText == value)
                {
                    return;
                }

                this.startDateText = value ?? string.Empty;
                ApplyFilters();
            }
        }

        protected string TypeFilterKey
        {
            get => this.typeFilterKey;
            set
            {
                if (this.typeFilterKey == value)
                {
                    return;
                }

                this.typeFilterKey = value ?? string.Empty;
                ApplyFilters();
            }
        }

        protected List<ConferenceView> Conferences { get; private set; } = new List<ConferenceView>();

        protected List<ConferenceView> FilteredConferences { get; private set; } = new List<ConferenceView>();

        protected List<ConferenceView> PagedConferences { get; private set; } = new List<ConferenceView>();

        protected int CurrentPage => this.currentPage;

        protected int TotalPages
        {
            get
            {
                if (this.FilteredConferences.Count == 0)
                {
                    return 1;
                }

                int count = this.FilteredConferences.Count;
                int pages = count / PageSize;

                if (count % PageSize != 0)
                {
                    pages++;
                }

                return pages;
            }
        }

        protected bool IsPreviousDisabled => this.CurrentPage <= 1;

        protected bool IsNextDisabled => this.CurrentPage >= this.TotalPages;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.Conferences = new List<ConferenceView>
            {
                new ConferenceView(
                    name: "Tech Innovation Summit 2024",
                    status: ConferenceStatus.Active,
                    startDate: new DateTime(2024, 3, 15),
                    endDate: new DateTime(2024, 3, 17),
                    locationText: "San Francisco, CA",
                    isVirtual: false,
                    description: "Annual technology conference featuring the latest innovations in AI, blockchain, and cloud computing.",
                    attendeeCount: 324,
                    createdAt: new DateTime(2024, 2, 1)),

                new ConferenceView(
                    name: "Global Marketing Conference",
                    status: ConferenceStatus.Virtual,
                    startDate: new DateTime(2024, 4, 22),
                    endDate: new DateTime(2024, 4, 24),
                    locationText: "Online Event",
                    isVirtual: true,
                    description: "Three-day virtual conference exploring digital marketing trends, consumer behavior, and brand strategy.",
                    attendeeCount: 156,
                    createdAt: new DateTime(2024, 3, 5)),

                new ConferenceView(
                    name: "Healthcare Innovation Forum",
                    status: ConferenceStatus.Upcoming,
                    startDate: new DateTime(2024, 5, 10),
                    endDate: new DateTime(2024, 5, 12),
                    locationText: "Chicago, IL",
                    isVirtual: false,
                    description: "Leading healthcare professionals discuss breakthrough treatments, medical technology, and patient care innovations.",
                    attendeeCount: 89,
                    createdAt: new DateTime(2024, 3, 20)),

                new ConferenceView(
                    name: "Sustainable Energy Symposium",
                    status: ConferenceStatus.Active,
                    startDate: new DateTime(2024, 6, 5),
                    endDate: new DateTime(2024, 6, 7),
                    locationText: "Austin, TX",
                    isVirtual: false,
                    description: "Exploring renewable energy solutions, environmental sustainability, and green technology innovations.",
                    attendeeCount: 267,
                    createdAt: new DateTime(2024, 4, 2))
            };

            this.FilteredConferences = new List<ConferenceView>(this.Conferences);
            UpdatePagedConferences();
        }

        private void ApplyFilters()
        {
            IEnumerable<ConferenceView> query = this.Conferences;

            string term = this.searchText.Trim();

            if (string.IsNullOrWhiteSpace(term) == false)
            {
                string lowered = term.ToLowerInvariant();

                query = query.Where(conference =>
                    Contains(conference.Name, lowered) ||
                    Contains(conference.LocationText, lowered) ||
                    Contains(conference.Description, lowered) ||
                    Contains(conference.TypeText, lowered));
            }

            DateTime parsedStartDate;

            if (string.IsNullOrWhiteSpace(this.startDateText) == false &&
                DateTime.TryParse(this.startDateText, out parsedStartDate))
            {
                query = query.Where(conference => conference.StartDate.Date >= parsedStartDate.Date);
            }

            string typeKey = this.typeFilterKey.Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(typeKey) == false)
            {
                if (typeKey == "virtual")
                {
                    query = query.Where(conference => conference.IsVirtual);
                }
                else if (typeKey == "in-person")
                {
                    query = query.Where(conference => conference.IsVirtual == false);
                }
            }

            this.FilteredConferences = query.ToList();
            this.currentPage = 1;
            UpdatePagedConferences();
            StateHasChanged();
        }

        private void UpdatePagedConferences()
        {
            int totalPages = this.TotalPages;

            if (this.currentPage < 1)
            {
                this.currentPage = 1;
            }

            if (this.currentPage > totalPages)
            {
                this.currentPage = totalPages;
            }

            int skip = (this.currentPage - 1) * PageSize;

            this.PagedConferences = this.FilteredConferences
                .Skip(skip)
                .Take(PageSize)
                .ToList();
        }

        protected void ClearFilters()
        {
            this.searchText = string.Empty;
            this.startDateText = string.Empty;
            this.typeFilterKey = string.Empty;

            this.FilteredConferences = new List<ConferenceView>(this.Conferences);
            this.currentPage = 1;

            UpdatePagedConferences();
            StateHasChanged();
        }

        protected void GoToPreviousPage()
        {
            if (this.IsPreviousDisabled)
            {
                return;
            }

            this.currentPage--;
            UpdatePagedConferences();
            StateHasChanged();
        }

        protected void GoToNextPage()
        {
            if (this.IsNextDisabled)
            {
                return;
            }

            this.currentPage++;
            UpdatePagedConferences();
            StateHasChanged();
        }

        protected void GoToPage(int page)
        {
            if (page < 1 || page > this.TotalPages)
            {
                return;
            }

            this.currentPage = page;
            UpdatePagedConferences();
            StateHasChanged();
        }

        protected void OnAddConferenceClicked()
        {
            // Placeholder for future integration (e.g., open modal, navigate to form)
        }

        protected void OnEditConference(ConferenceView conference)
        {
            // Placeholder for edit logic
        }

        protected void OnDeleteConference(ConferenceView conference)
        {
            // Placeholder for delete logic
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
