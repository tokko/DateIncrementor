using System;
using System.Collections.Generic;

namespace DateIncrementor
{
    public class DateIncrementor
    {
        private DateTime _startDate;
        private DateTime? _endDate;
        private int _yearIncrement;
        private int _monthIncrement;
        private int _weekIncrement;
        private int? _weekDay;
        private bool _onLastDayOfMonth;
        private int? _day;
        private int? _month;

        public DateIncrementor WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public DateIncrementor WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public DateIncrementor WithYearIncrement(int yearIncrement)
        {
            _yearIncrement = yearIncrement;
            return this;
        }

        public DateIncrementor WithMonthIncrement(int monthIncrement)
        {
            _monthIncrement = monthIncrement;
            return this;
        }

        public DateIncrementor WithWeekIncrement(int weekIncrement)
        {
            _weekIncrement = weekIncrement;
            return this;
        }

        public DateIncrementor OnDayOfWeek(int weekDay)
        {
            _weekDay = weekDay + 1;
            return this;
        }

        public DateIncrementor OnMonth(int month)
        {
            _month = month;
            return this;
        }

        public DateIncrementor OnDay(int day)
        {
            _day = day;
            return this;
        }
        public DateIncrementor OnLastDayOfMonth()
        {
            _onLastDayOfMonth = true;
            return this;
        }

        public IEnumerable<DateTime> GetDates()
        {
            var date = _startDate;
            date = AdjustDate(date);
            if (date >= _startDate)
                yield return date;
            while (date <= (_endDate ?? DateTime.MaxValue))
            {
                date = date.AddYears(_yearIncrement);
                date = date.AddMonths(_monthIncrement);
                date = date.AddDays(7 * _weekIncrement);
                date = AdjustDate(date);
                yield return date;
            }
        }

        private DateTime AdjustDate(DateTime date)
        {
            if (_weekDay.HasValue)
            {
                for (; date.AddDays(-1).DayOfWeek > (DayOfWeek)_weekDay.Value; date = date.AddDays(-1)) ;
                for (; (int)date.AddDays(1).DayOfWeek < _weekDay.Value; date = date.AddDays(1)) ;
            }
            if (_month.HasValue)
            {
                for (; date.AddMonths(-1).Month > _month.Value; date = date.AddMonths(-1)) ;
                for (; date.AddMonths(1).Month < _month.Value; date = date.AddMonths(1)) ;
            }
            if (_day.HasValue)
            {
                for (; date.AddDays(-1).Day > _day.Value && date.AddDays(-1).Month == date.Month; date = date.AddDays(-1)) ;
                for (; date.AddDays(1).Day <= _day.Value && date.AddDays(1).Month == date.Month; date = date.AddDays(1)) ;
            }
            if (_onLastDayOfMonth)
                for (; date.AddDays(1).Month == date.Month; date = date.AddDays(1)) ;

            return date;
        }
    }
}
