using System;

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
            var totalDay = end.Day - start.Day + 1;
            var budget = _budgetRepo.GetAll().Find(x=>x.YearMonth == $"{start.Date.Year}{start.Date.Month}");
            var amountPerDay = budget.Amount / totalDay;
            return amountPerDay * totalDay;
        }
    }
}