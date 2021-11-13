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
                new() { YearMonth = "202111", Amount = 3000 }
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
    }
}