using Bunit;
using Xunit;
using Upc.Web.Views.Components.AttendeesHeroes;
using FluentAssertions;
using Upc.Web.Models.Components.Attendees;

namespace Upc.Web.Tests.Unit
{
    public class AttendeesHeroV1InstanceStylesTests : TestContext
    {
        private IRenderedComponent<AttendeesHeroComponentV1> renderedAttendeesHeroComponentV1;

        [Fact]
        public void AttendeesHeroComponentV1_ShouldExposeStyleInstanceWithExpectedValues()
        {
            // given .. when
            this.renderedAttendeesHeroComponentV1 = 
                RenderComponent<AttendeesHeroComponentV1>();

            AttendeesHeroComponentStyle styles = 
                renderedAttendeesHeroComponentV1.Instance.Style;

            // then

            styles.Should().NotBeNull();

            styles.AttendeesHero.TextAlign.Should().Be("left");
            styles.AttendeesHero.MarginBottom.Should().Be("2.5rem");

            styles.AttendeesHeroInner.MaxWidth.Should().Be("40rem"); 

            styles.AttendeesHeroTitle.Margin.Should().Be("0 0 1rem 0");
            styles.AttendeesHeroTitle.FontSize.Should().Be("2rem");
            styles.AttendeesHeroTitle.FontWeight.Should().Be("700");
            styles.AttendeesHeroTitle.Color.Should().Be("#111827");

            styles.AttendeesHeroText.Margin.Should().Be("0 0 1.75rem 0");
            styles.AttendeesHeroText.FontSize.Should().Be("1rem");
            styles.AttendeesHeroText.LineHeight.Should().Be("1.6");
            styles.AttendeesHeroText.Color.Should().Be("#6C757D"); 

            styles.AttendeesHeroDivider.Width.Should().Be("4rem");
            styles.AttendeesHeroDivider.Height.Should().Be("0.22rem");
            styles.AttendeesHeroDivider.BorderRadius.Should().Be("999px");
            styles.AttendeesHeroDivider.BackgroundColor.Should().Be("#007BFF");
        }
    }
}