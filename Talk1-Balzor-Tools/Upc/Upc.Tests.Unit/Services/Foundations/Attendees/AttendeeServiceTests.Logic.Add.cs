// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Upc.Models.Foundations.Attendees;
using Xunit;

namespace Upc.Tests.Unit.Services.Foundations.Attendees
{
    public partial class AttendeeServiceTests
    {
        [Fact]
        public async Task ShouldAddAttendeeAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            Attendee randomAttendee = CreateRandomAttendee(randomDateTimeOffset);
            Attendee inputAttendee = randomAttendee;
            Attendee storageAttendee = inputAttendee;
            Attendee expectedAttendee = storageAttendee.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertAttendeeAsync(inputAttendee))
                    .ReturnsAsync(storageAttendee);

            // when
            Attendee actualAttendee = await this.attendeeService
                .AddAttendeeAsync(inputAttendee);

            // then
            actualAttendee.Should().BeEquivalentTo(expectedAttendee);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertAttendeeAsync(inputAttendee),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}