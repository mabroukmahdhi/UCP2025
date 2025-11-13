using System.Linq;
using FluentAssertions;
using Moq;
using Upc.Models.Foundations.Conferences;
using Xunit;

namespace Upc.Tests.Unit.Services.Foundations.Conferences
{
    public partial class ConferenceServiceTests
    {
        [Fact]
        public void ShouldReturnConferences()
        {
            // given
            IQueryable<Conference> randomConferences = CreateRandomConferences();
            IQueryable<Conference> storageConferences = randomConferences;
            IQueryable<Conference> expectedConferences = storageConferences;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllConferences())
                    .Returns(storageConferences);

            // when
            IQueryable<Conference> actualConferences =
                this.conferenceService.RetrieveAllConferences();

            // then
            actualConferences.Should().BeEquivalentTo(expectedConferences);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllConferences(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}