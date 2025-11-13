// ----------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Made with love for Update Conference Prague 2025.
// ----------------------------------------------------

using System.Linq;
using FluentAssertions;
using Moq;
using Upc.Models.Foundations.Attendees;
using Xunit;

namespace Upc.Tests.Unit.Services.Foundations.Attendees
{
    public partial class AttendeeServiceTests
    {
        [Fact]
        public void ShouldReturnAttendees()
        {
            // given
            IQueryable<Attendee> randomAttendees = CreateRandomAttendees();
            IQueryable<Attendee> storageAttendees = randomAttendees;
            IQueryable<Attendee> expectedAttendees = storageAttendees;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllAttendees())
                    .Returns(storageAttendees);

            // when
            IQueryable<Attendee> actualAttendees =
                this.attendeeService.RetrieveAllAttendees();

            // then
            actualAttendees.Should().BeEquivalentTo(expectedAttendees);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllAttendees(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}