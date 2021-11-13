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
                new() { YearMonth = "202111", Amount = 3100 },
            });
            var startDataTime = new DateTime(2021, 11, 01);
            var endDataTime = new DateTime(2021, 11, 31);
            var budgetService = new BudgetService(budgetRepo);
            Assert.AreEqual(budgetService.Query(startDataTime, endDataTime), 3000);
        }
    }
}