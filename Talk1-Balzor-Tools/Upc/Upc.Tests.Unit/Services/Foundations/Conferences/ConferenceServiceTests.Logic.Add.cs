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
        public async Task ShouldAddConferenceAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            Conference randomConference = CreateRandomConference(randomDateTimeOffset);
            Conference inputConference = randomConference;
            Conference storageConference = inputConference;
            Conference expectedConference = storageConference.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertConferenceAsync(inputConference))
                    .ReturnsAsync(storageConference);

            // when
            Conference actualConference = await this.conferenceService
                .AddConferenceAsync(inputConference);

            // then
            actualConference.Should().BeEquivalentTo(expectedConference);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertConferenceAsync(inputConference),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}