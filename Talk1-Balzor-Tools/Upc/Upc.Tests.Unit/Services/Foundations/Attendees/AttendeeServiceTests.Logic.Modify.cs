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
        public async Task ShouldModifyAttendeeAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            Attendee randomAttendee = CreateRandomModifyAttendee(randomDateTimeOffset);
            Attendee inputAttendee = randomAttendee;
            Attendee storageAttendee = inputAttendee.DeepClone();
            storageAttendee.UpdatedDate = randomAttendee.CreatedDate;
            Attendee updatedAttendee = inputAttendee;
            Attendee expectedAttendee = updatedAttendee.DeepClone();
            Guid attendeeId = inputAttendee.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAttendeeByIdAsync(attendeeId))
                    .ReturnsAsync(storageAttendee);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateAttendeeAsync(inputAttendee))
                    .ReturnsAsync(updatedAttendee);

            // when
            Attendee actualAttendee =
                await this.attendeeService.ModifyAttendeeAsync(inputAttendee);

            // then
            actualAttendee.Should().BeEquivalentTo(expectedAttendee);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAttendeeByIdAsync(inputAttendee.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateAttendeeAsync(inputAttendee),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}