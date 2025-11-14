// --------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// --------------------------------------------------------

using System;
using System.IO;

namespace Upc.Web.Tests.Unit
{
    public class AttendeesHeroStylesTests
    {
        [Fact]
        public void AttendeesHeroComponent_ShouldContainExpectedStyles()
        {
            // Compute path from test output directory back to the project layout
            string filePath = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory,
                    "..", "..", "..", "..",
                    "Upc.Web",
                    "Views", "Components", "AttendeesHeroes", "AttendeesHeroComponent.razor"));

            Assert.True(File.Exists(filePath), $"Razor file not found at: {filePath}");

            string content = File.ReadAllText(filePath);

            // Ensure style block exists
            Assert.Contains("<style>", content);
            Assert.Contains(".cn-attendees-hero", content);
            Assert.Contains(".cn-attendees-hero-title", content);
            Assert.Contains(".cn-attendees-hero-text", content);
            Assert.Contains(".cn-attendees-hero-divider", content);

            // Check a couple of specific style rules
            Assert.Contains("margin-bottom: 2.5rem", content);
            Assert.Contains("font-size: 2rem", content);
            Assert.Contains("background-color: #007BFF", content);
        }
    }
}