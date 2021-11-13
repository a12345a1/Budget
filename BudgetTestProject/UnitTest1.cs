using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BudgetTestProject
{
    public class Tests
    {
        [Test]
        public void CalculateOneMonth()
        {
            var budgetRepo = Substitute.For<IBudgetRepo>();
            budgetRepo.GetAll().Returns(new List<Budget>()
            {
                new() { YearMonth = "202111", Amount = 3000 },
            });
            var start = new DateTime(2021, 11, 01);
            var end = new DateTime(start.AddMonths(1).Year, start.AddMonths(1).Month, 1).AddDays(-1);
            var budgetService = new BudgetService(budgetRepo);
            Assert.AreEqual(budgetService.Query(start, end), 3000);
        }
    }
}