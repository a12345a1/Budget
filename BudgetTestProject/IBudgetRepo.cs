using System.Collections.Generic;

namespace BudgetTestProject
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}