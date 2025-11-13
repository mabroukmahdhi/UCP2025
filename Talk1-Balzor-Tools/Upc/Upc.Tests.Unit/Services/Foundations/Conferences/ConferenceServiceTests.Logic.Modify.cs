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
        public async Task ShouldModifyConferenceAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            Conference randomConference = CreateRandomModifyConference(randomDateTimeOffset);
            Conference inputConference = randomConference;
            Conference storageConference = inputConference.DeepClone();
            storageConference.UpdatedDate = randomConference.CreatedDate;
            Conference updatedConference = inputConference;
            Conference expectedConference = updatedConference.DeepClone();
            Guid conferenceId = inputConference.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectConferenceByIdAsync(conferenceId))
                    .ReturnsAsync(storageConference);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateConferenceAsync(inputConference))
                    .ReturnsAsync(updatedConference);

            // when
            Conference actualConference =
                await this.conferenceService.ModifyConferenceAsync(inputConference);

            // then
            actualConference.Should().BeEquivalentTo(expectedConference);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectConferenceByIdAsync(inputConference.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateConferenceAsync(inputConference),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}