using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Upc.Models.Foundations.Conferences;
using Xunit;

namespace Upc.Tests.Unit.Services.Foundations.Conferences
{
    public partial class ConferenceServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveConferenceByIdAsync()
        {
            // given
            Conference randomConference = CreateRandomConference();
            Conference inputConference = randomConference;
            Conference storageConference = randomConference;
            Conference expectedConference = storageConference.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectConferenceByIdAsync(inputConference.Id))
                    .ReturnsAsync(storageConference);

            // when
            Conference actualConference =
                await this.conferenceService.RetrieveConferenceByIdAsync(inputConference.Id);

            // then
            actualConference.Should().BeEquivalentTo(expectedConference);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectConferenceByIdAsync(inputConference.Id),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}