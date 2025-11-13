using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Moq;
using Upc.Brokers.DateTimes;
using Upc.Brokers.Loggings;
using Upc.Brokers.Storages;
using Upc.Models.Foundations.Conferences; 
using Upc.Services.Foundations.Conferences;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Upc.Tests.Unit.Services.Foundations.Conferences
{
    public partial class ConferenceServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ConferenceService conferenceService;

        public ConferenceServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.conferenceService = new ConferenceService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData<int> MinutesBeforeOrAfter()
        {
            int randomNumber = GetRandomNumber();
            int randomNegativeNumber = GetRandomNegativeNumber();

            return new TheoryData<int>
            {
                randomNumber,
                randomNegativeNumber
            };
        }

        private static SqlException CreateSqlException() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetRandomNegativeNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Exception expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Conference CreateRandomConference(DateTimeOffset dates) =>
            CreateConferenceFiller(dates).Create();

        private static Conference CreateRandomConference() =>
            CreateConferenceFiller(dates: GetRandomDateTimeOffset()).Create();

        private static IQueryable<Conference> CreateRandomConferences()
        {
            return CreateConferenceFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        private static Conference CreateRandomModifyConference(DateTimeOffset dates)
        {
            int randomDaysInPast = GetRandomNumber();
            Conference randomConference = CreateRandomConference(dates);

            randomConference.CreatedDate =
                randomConference.CreatedDate.AddDays(randomDaysInPast);

            return randomConference;
        }

        private static Filler<Conference> CreateConferenceFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Conference>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}