// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;
using SharpStyles.Models.Attributes;
using SharpStyles.Models.Queries;

namespace Upc.Web.Views.Components.Tests
{
    public partial class TestComponent : ComponentBase
    {
        public MyComponentStyle Style { get; set; }

        protected override void OnInitialized()
        {
            this.Style = new MyComponentStyle
            {
                H3 = new SharpStyle
                {
                    Color = "red"
                },

                PrimaryButton = new SharpStyle
                {
                    BackgroundColor = "blue",
                    Color = "white"
                },

                SubmitButton = new SharpStyle
                {
                    Width = "12px"
                },

                MediaQueries = SetupMediaQueries()
            };
        }

        private static List<MediaQuery> SetupMediaQueries()
        {
            return new List<MediaQuery>
                {
                    new MediaQuery
                    {
                        Only = true,
                        MediaType = "screen",
                        MaxWidth = 768,
                        Styles = new MyComponentStyle
                        {
                            H3 = new SharpStyle
                            {
                                Color = "green"
                            }
                        }
                    }
                };
        }
    }
}