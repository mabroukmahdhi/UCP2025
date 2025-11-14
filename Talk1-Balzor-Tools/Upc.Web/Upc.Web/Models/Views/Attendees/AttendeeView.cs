// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;

namespace Upc.Web.Models.Views.Attendees
{
    public class AttendeeView
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
