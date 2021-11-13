using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetTestProject
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public double Query(DateTime start, DateTime end)
        {
            var money = 0;
            var totalDay = GetAllDateTime(start, end);
            var monthGroup = totalDay.GroupBy(x => x.Month);
            using var enumerator = monthGroup.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current.First();
                var totalDaysInMonth = DateTime.DaysInMonth(item.Year, item.Month);
                var budget = GetBudget(item.Year, item.Month);
                var amountPerDay = budget.Amount / totalDaysInMonth;
                money += amountPerDay * enumerator.Current.Count();
            }

            return money;
        }

        private IEnumerable<DateTime> GetAllDateTime(DateTime start, DateTime end)
        {
            for (var dt = start; dt.CompareTo(end) <= 0; dt = dt.AddDays(1))
            {
                yield return dt;
            }
        }

        private Budget GetBudget(int year, int month)
        {
            return _budgetRepo.GetAll().Find(x => x.YearMonth == $"{year}{month.ToString("00")}");
        }
    }
}