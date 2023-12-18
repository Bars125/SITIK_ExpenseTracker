using MyApp.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

public class StatisticsVM
{
    public ObservableCollection<ExpenseModel> Expenses { get; set; }
    public ObservableCollection<IncomeModel> Incomes { get; set; }

    public double TotalExpenses => Expenses?.Sum(expense => expense.Amount) ?? 0;
    public double TotalIncomes => Incomes?.Sum(income => income.Amount) ?? 0;
    public double Summary => TotalIncomes - TotalExpenses;

    public StatisticsVM()
    {
        Expenses = new ObservableCollection<ExpenseModel>();
        Incomes = new ObservableCollection<IncomeModel>();
    }
}
