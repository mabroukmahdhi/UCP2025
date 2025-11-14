// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;

namespace Upc.Web.Models.Views.Conferences
{
    public class ConferenceView
    {
        public ConferenceView(
            string name,
            ConferenceStatus status,
            DateTime startDate,
            DateTime endDate,
            string locationText,
            bool isVirtual,
            string description,
            int attendeeCount,
            DateTime createdAt)
        {
            this.Name = name;
            this.Status = status;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.LocationText = locationText;
            this.IsVirtual = isVirtual;
            this.Description = description;
            this.AttendeeCount = attendeeCount;
            this.CreatedAt = createdAt;

            this.StatusText = BuildStatusText(status);
            this.StatusBadgeClass = BuildStatusBadgeClass(status);
            this.TypeText = isVirtual ? "Virtual" : "In-Person";
            this.TypeChipClass = BuildTypeChipClass(isVirtual);
            this.LocationIconClass = isVirtual
                ? "fa-solid fa-globe cn-conference-meta-icon"
                : "fa-solid fa-location-dot cn-conference-meta-icon";
            this.TypeIconClass = isVirtual
                ? "fa-solid fa-laptop cn-conference-meta-icon"
                : "fa-solid fa-building cn-conference-meta-icon";
            this.DateRangeText = BuildDateRangeText(startDate, endDate);
            this.AttendeeCountText = $"{attendeeCount} Attendees";
            this.CreatedAtText = $"Created: {createdAt:MMM d, yyyy}";
        }

        public string Name { get; }

        public ConferenceStatus Status { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public string LocationText { get; }

        public bool IsVirtual { get; }

        public string Description { get; }

        public int AttendeeCount { get; }

        public DateTime CreatedAt { get; }

        public string StatusText { get; }

        public string StatusBadgeClass { get; }

        public string TypeText { get; }

        public string TypeChipClass { get; }

        public string LocationIconClass { get; }

        public string TypeIconClass { get; }

        public string DateRangeText { get; }

        public string AttendeeCountText { get; }

        public string CreatedAtText { get; }

        private static string BuildStatusText(ConferenceStatus status)
        {
            if (status == ConferenceStatus.Active)
            {
                return "Active";
            }

            if (status == ConferenceStatus.Virtual)
            {
                return "Virtual";
            }

            if (status == ConferenceStatus.Upcoming)
            {
                return "Upcoming";
            }

            return status.ToString();
        }

        private static string BuildStatusBadgeClass(ConferenceStatus status)
        {
            string baseClass = "cn-conference-status-badge";

            if (status == ConferenceStatus.Active)
            {
                return baseClass + " cn-conference-status-active";
            }

            if (status == ConferenceStatus.Virtual)
            {
                return baseClass + " cn-conference-status-virtual";
            }

            if (status == ConferenceStatus.Upcoming)
            {
                return baseClass + " cn-conference-status-upcoming";
            }

            return baseClass;
        }

        private static string BuildTypeChipClass(bool isVirtual)
        {
            string baseClass = "cn-conference-type-chip";

            if (isVirtual)
            {
                return baseClass + " cn-conference-type-virtual";
            }

            return baseClass + " cn-conference-type-inperson";
        }

        private static string BuildDateRangeText(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date == endDate.Date)
            {
                return startDate.ToString("MMM d, yyyy");
            }

            if (startDate.Year == endDate.Year && startDate.Month == endDate.Month)
            {
                return startDate.ToString("MMM d") + " - " + endDate.ToString("d, yyyy");
            }

            return startDate.ToString("MMM d, yyyy") + " - " + endDate.ToString("MMM d, yyyy");
        }
    }
}
