using System;
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
        public async Task ShouldRemoveConferenceByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputConferenceId = randomId;
            Conference randomConference = CreateRandomConference();
            Conference storageConference = randomConference;
            Conference expectedInputConference = storageConference;
            Conference deletedConference = expectedInputConference;
            Conference expectedConference = deletedConference.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectConferenceByIdAsync(inputConferenceId))
                    .ReturnsAsync(storageConference);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteConferenceAsync(expectedInputConference))
                    .ReturnsAsync(deletedConference);

            // when
            Conference actualConference = await this.conferenceService
                .RemoveConferenceByIdAsync(inputConferenceId);

            // then
            actualConference.Should().BeEquivalentTo(expectedConference);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectConferenceByIdAsync(inputConferenceId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteConferenceAsync(expectedInputConference),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}