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
                Assert.That(dateTime.Year, Is.EqualTo(2017+i-2));
                Assert.That(dateTime.Month, Is.EqualTo(2));
                Assert.That(dateTime.Day, Is.EqualTo(24));
            }

        }
    }
}
