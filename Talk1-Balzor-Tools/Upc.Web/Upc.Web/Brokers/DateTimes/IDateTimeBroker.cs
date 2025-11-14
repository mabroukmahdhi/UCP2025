using System;

namespace Upc.Web.Brokers.DateTimes
{
    public interface IDateTimeBroker
    {
        DateTimeOffset GetCurrentDateTimeOffset();
    }
}