using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BudgetTestProject
{
    public class Tests
    {
        private IBudgetRepo _budgetRepo;

        [SetUp]
        public void SetUp()
        {
            _budgetRepo = Substitute.For<IBudgetRepo>();
            _budgetRepo.GetAll().Returns(new List<Budget>
            {
                new() { YearMonth = "202109", Amount = 3000 },
                new() { YearMonth = "202110", Amount = 3100 },
                new() { YearMonth = "202111", Amount = 3000 },
                new() { YearMonth = "202112", Amount = 6200 },
            });
        }

        [Test]
        public void CalculateOneMonth()
        {
            var start = new DateTime(2021, 11, 01);
            var end = new DateTime(start.AddMonths(1).Year, start.AddMonths(1).Month, 1).AddDays(-1);
            var budgetService = new BudgetService(_budgetRepo);
            Assert.AreEqual(budgetService.Query(start, end), 3000);
        }

        [Test]
        public void CalculateOneDay()
        {
            var start = new DateTime(2021, 11, 01);
            var end = new DateTime(2021, 11, 01);
            var budgetService = new BudgetService(_budgetRepo);
            Assert.AreEqual(budgetService.Query(start, end), 100);
        }

        [Test]
        public void CalculateCrossMouth()
        {
            var start = new DateTime(2021, 9, 15);
            var end = new DateTime(2021, 12, 15);
            var budgetService = new BudgetService(_budgetRepo);
            Assert.AreEqual(budgetService.Query(start, end), 10700);
        }
    }
}