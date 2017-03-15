using System;
using System.Linq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Weekly()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-02-14")).OnDayOfWeek(2).WithWeekIncrement(1);

            var res = incrementor.GetDates().Take(4).ToList();

            Assert.NotNull(res);
            Assert.That(res.Count, Is.EqualTo(4));
            Assert.That(res.First().Year, Is.EqualTo(2017));
            Assert.That(res.First().Month, Is.EqualTo(2));
            Assert.That(res.First().Day, Is.EqualTo(14));
        }

        [Test]
        public void Monthly()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-02-14")).WithMonthIncrement(1);

            var res = incrementor.GetDates().Take(4).ToList();

            Assert.NotNull(res);
            Assert.That(res.Count, Is.EqualTo(4));

            for (var i = 2; i <= 4; i++)
            {
                var dateTime = res[i - 2];
                Assert.That(dateTime.Year, Is.EqualTo(2017));
                Assert.That(dateTime.Month, Is.EqualTo(i));
                Assert.That(dateTime.Day, Is.EqualTo(14));
            }

        }

        [Test]
        public void Yearly()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-02-14")).WithYearIncrement(1).OnMonth(2).OnDay(24);

            var res = incrementor.GetDates().Take(4).ToList();

            Assert.NotNull(res);
            Assert.That(res.Count, Is.EqualTo(4));

            for (var i = 2; i <= 4; i++)
            {
                var dateTime = res[i - 2];
                Assert.That(dateTime.Year, Is.EqualTo(2017 + i - 2));
                Assert.That(dateTime.Month, Is.EqualTo(2));
                Assert.That(dateTime.Day, Is.EqualTo(24));
            }
        }

        [Test]
        public void Monthly_Day_Exceeds_30()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-04-1")).OnDay(31).WithMonthIncrement(1);

            var res = incrementor.GetDates().Take(1).ToList();

            Assert.NotNull(res);
            var dateTime = res.Single();
            Assert.That(dateTime.Year, Is.EqualTo(2017));
            Assert.That(dateTime.Month, Is.EqualTo(4));
            Assert.That(dateTime.Day, Is.EqualTo(30));
        }

        [Test]
        public void Monthly_Day_FebruaryExceeds28()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-02-2")).OnDay(29).WithMonthIncrement(1);

            var res = incrementor.GetDates().Take(1).ToList();

            Assert.NotNull(res);
            var dateTime = res.Single();
            Assert.That(dateTime.Year, Is.EqualTo(2017));
            Assert.That(dateTime.Month, Is.EqualTo(2));
            Assert.That(dateTime.Day, Is.EqualTo(28));
        }

        [Test]
        public void Monthly_LastDayOfMonth()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-01-1")).OnLastDayOfMonth().WithMonthIncrement(1);

            var res = incrementor.GetDates().Take(4).ToList();

            Assert.That(res[0].Day, Is.EqualTo(31));
            Assert.That(res[1].Day, Is.EqualTo(28));
            Assert.That(res[2].Day, Is.EqualTo(31));
            Assert.That(res[3].Day, Is.EqualTo(30));

        }

        [Test]
        public void Monthly_EveryOtherMonth()
        {
            var incrementor = new DateIncrementor.DateIncrementor();

            incrementor.WithStartDate(DateTime.Parse("2017-01-1")).OnLastDayOfMonth().WithMonthIncrement(2);

            var res = incrementor.GetDates().Take(4).ToList();

            Assert.That(res[0].Month, Is.EqualTo(1));
            Assert.That(res[1].Month, Is.EqualTo(3));

        }
    }
}
