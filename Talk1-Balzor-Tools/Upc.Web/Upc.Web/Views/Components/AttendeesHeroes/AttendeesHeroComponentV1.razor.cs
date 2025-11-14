// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;
using Upc.Web.Models.Components.Attendees;

namespace Upc.Web.Views.Components.AttendeesHeroes
{
    public partial class AttendeesHeroComponentV1 : ComponentBase
    {
        public AttendeesHeroComponentStyle Style { get; set; }

        protected override void OnInitialized() =>
            this.Style = SetupStyles();

        private static AttendeesHeroComponentStyle SetupStyles()
        { 
            return new AttendeesHeroComponentStyle
            {
                AttendeesHero = new SharpStyle
                {
                    TextAlign = "left",
                    MarginBottom = "2.5rem"
                },

                AttendeesHeroInner = new SharpStyle
                {
                    MaxWidth = "40rem"
                },

                AttendeesHeroTitle = new SharpStyle
                {
                    Margin = "0 0 1rem 0",
                    FontSize = "2rem",
                    FontWeight = "700",
                    Color = "#111827"
                },

                AttendeesHeroText = new SharpStyle
                {
                    Margin = "0 0 1.75rem 0",
                    FontSize = "1rem",
                    LineHeight = "1.6",
                    Color = "#6C757D"
                },

                AttendeesHeroDivider = new SharpStyle
                {
                    Width = "4rem",
                    Height = "0.22rem",
                    BorderRadius = "999px",
                    BackgroundColor = "#007BFF"
                }
            };
        }
    }
}