using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.ViewModels
{
    public class ExpenseModel
    {
        public int Amount { get; set; }
        public string Category { get; set; }
    }
}
