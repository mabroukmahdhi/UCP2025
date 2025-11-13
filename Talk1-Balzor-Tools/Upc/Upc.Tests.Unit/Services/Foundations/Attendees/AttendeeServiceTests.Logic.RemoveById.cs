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
        public async Task ShouldRemoveAttendeeByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputAttendeeId = randomId;
            Attendee randomAttendee = CreateRandomAttendee();
            Attendee storageAttendee = randomAttendee;
            Attendee expectedInputAttendee = storageAttendee;
            Attendee deletedAttendee = expectedInputAttendee;
            Attendee expectedAttendee = deletedAttendee.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAttendeeByIdAsync(inputAttendeeId))
                    .ReturnsAsync(storageAttendee);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteAttendeeAsync(expectedInputAttendee))
                    .ReturnsAsync(deletedAttendee);

            // when
            Attendee actualAttendee = await this.attendeeService
                .RemoveAttendeeByIdAsync(inputAttendeeId);

            // then
            actualAttendee.Should().BeEquivalentTo(expectedAttendee);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAttendeeByIdAsync(inputAttendeeId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteAttendeeAsync(expectedInputAttendee),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}