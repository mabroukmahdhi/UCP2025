// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.IO;
using Bunit;
using Upc.Web.Views.Components.AttendeesHeroes;

namespace Upc.Web.Tests.Unit
{
    public class AttendeesHeroStylesTests : TestContext
    {
        private IRenderedComponent<AttendeesHeroComponent> renderedAttendeesHeroComponent; 

        [Fact]
        public void AttendeesHeroComponent_ShouldContainExpectedStyles()
        {
            // given
            string filePath = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "..",
                    "Upc.Web",
                    "Views", "Components", "AttendeesHeroes", "AttendeesHeroComponent.razor"));

            Assert.True(File.Exists(filePath), $"Razor file not found at: {filePath}");

            // when
            string content = File.ReadAllText(filePath);

            // then
            Assert.Contains("<style>", content);
            Assert.Contains(".cn-attendees-hero", content);
            Assert.Contains(".cn-attendees-hero-title", content);
            Assert.Contains(".cn-attendees-hero-text", content);
            Assert.Contains(".cn-attendees-hero-divider", content);
            Assert.Contains("margin-bottom: 2.5rem", content);
            Assert.Contains("font-size: 2rem", content);
            Assert.Contains("background-color: #007BFF", content);
        }

        [Fact]
        public void AttendeesHeroComponent_ShouldContainExpectedStylesV1()
        {
            // given .. when
            this.renderedAttendeesHeroComponent = RenderComponent<AttendeesHeroComponent>();

            // then
            var styleElement = renderedAttendeesHeroComponent.Find("style");

            string css = styleElement.InnerHtml;

            Assert.Contains(".cn-attendees-hero", css);
            Assert.Contains(".cn-attendees-hero-inner", css);
            Assert.Contains(".cn-attendees-hero-title", css);
            Assert.Contains(".cn-attendees-hero-text", css);
            Assert.Contains(".cn-attendees-hero-divider", css);
            Assert.Contains("margin-bottom: 2.5rem", css);
            Assert.Contains("font-size: 2rem", css);
            Assert.Contains("background-color: #007BFF", css);
        }
    }
}