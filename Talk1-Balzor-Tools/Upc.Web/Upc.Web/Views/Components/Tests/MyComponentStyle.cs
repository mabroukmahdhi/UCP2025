// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace Upc.Web.Views.Components.Tests
{
    public class MyComponentStyle : SharpStyle
    {
        [CssElement]
        public SharpStyle H3 { get; set; }

        [CssClass]
        public SharpStyle PrimaryButton { get; set; }

        [CssId]
        public SharpStyle SubmitButton { get; set; }
    }
}