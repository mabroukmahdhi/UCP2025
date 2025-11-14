// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace Upc.Web.Models.Components.Attendees
{
    public class AttendeesHeroComponentStyle : SharpStyle
    { 

        [CssClass(Selector = ".cn-attendees-hero")]
        public SharpStyle AttendeesHero { get; set; }

        [CssClass(Selector = ".cn-attendees-hero-inner")]
        public SharpStyle AttendeesHeroInner { get; set; }

        [CssClass(Selector = ".cn-attendees-hero-title")]
        public SharpStyle AttendeesHeroTitle { get; set; }

        [CssClass(Selector = ".cn-attendees-hero-text")]
        public SharpStyle AttendeesHeroText { get; set; }

        [CssClass(Selector = ".cn-attendees-hero-divider")]
        public SharpStyle AttendeesHeroDivider { get; set; }
    }
}
