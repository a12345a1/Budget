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
            var monthsGroup = totalDay.GroupBy(x => x.Month);
            using var enumerator = monthsGroup.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var perMonthGroup = enumerator.Current;
                var item = perMonthGroup.First();
                var daysInMonth = perMonthGroup.Count();
                money += CalculateMoney(item, daysInMonth);
            }

            return money;
        }

        private int CalculateMoney(DateTime item, int daysInMonth)
        {
            var totalDaysInMonth = DateTime.DaysInMonth(item.Year, item.Month);
            var budgetAmount = GetBudgetAmount(item.Year, item.Month);
            var amountPerDay = budgetAmount / totalDaysInMonth;
            return amountPerDay * daysInMonth;
        }

        private IEnumerable<DateTime> GetAllDateTime(DateTime start, DateTime end)
        {
            for (var dt = start; dt.CompareTo(end) <= 0; dt = dt.AddDays(1))
            {
                yield return dt;
            }
        }

        private int GetBudgetAmount(int year, int month)
        {
            var budget = _budgetRepo.GetAll().FirstOrDefault(x => x.YearMonth == $"{year}{month:00}");
            if (budget == null) return 0;
            return budget.Amount;
        }
    }
}