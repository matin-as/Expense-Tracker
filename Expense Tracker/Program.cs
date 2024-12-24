using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
class Program
{
    static string DataFilePath = "expenses.json";

    static void Main(string[] args)
    {
        if (!File.Exists(DataFilePath))
        {
            File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(new List<Expense>()));
        }

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a command.");
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "add":
                AddExpense(args);
                break;
            case "list":
                ListExpenses();
                break;
            case "summary":
                ShowSummary(args);
                break;
            case "delete":
                DeleteExpense(args);
                break;
            default:
                Console.WriteLine("Invalid command.");
                break;
        }
    }

    static void AddExpense(string[] args)
    {
        string description = null;
        decimal amount = 0;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--description" && i + 1 < args.Length)
            {
                description = args[i + 1];
                i++;
            }
            else if (args[i] == "--amount" && i + 1 < args.Length && decimal.TryParse(args[i + 1], out amount))
            {
                i++;
            }
        }

        if (string.IsNullOrEmpty(description) || amount <= 0)
        {
            Console.WriteLine("Invalid input. Use: add --description <description> --amount <amount>");
            return;
        }

        var expenses = LoadExpenses();
        var newExpense = new Expense
        {
            Id = expenses.Any() ? expenses.Max(e => e.Id) + 1 : 1,
            Description = description,
            Amount = amount,
            Date = DateTime.Now
        };

        expenses.Add(newExpense);
        SaveExpenses(expenses);

        Console.WriteLine($"Expense added successfully (ID: {newExpense.Id})");
    }

    static void ListExpenses()
    {
        var expenses = LoadExpenses();
        if (!expenses.Any())
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        Console.WriteLine("ID  Date       Description        Amount");
        foreach (var expense in expenses)
        {
            Console.WriteLine($"{expense.Id}   {expense.Date.ToString("yyyy-MM-dd")}  {expense.Description,-15}  ${expense.Amount}");
        }
    }

    static void ShowSummary(string[] args)
    {
        var expenses = LoadExpenses();
        if (args.Length > 1 && args[1] == "--month" && args.Length > 2 && int.TryParse(args[2], out int month))
        {
            var monthlyExpenses = expenses.Where(e => e.Date.Month == month && e.Date.Year == DateTime.Now.Year).ToList();
            decimal total = monthlyExpenses.Sum(e => e.Amount);
            Console.WriteLine($"Total expenses for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}: ${total}");
        }
        else
        {
            decimal total = expenses.Sum(e => e.Amount);
            Console.WriteLine($"Total expenses: ${total}");
        }
    }

    static void DeleteExpense(string[] args)
    {
        if (args.Length < 3 || args[1] != "--id" || !int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Invalid input. Use: delete --id <id>");
            return;
        }

        var expenses = LoadExpenses();
        var expense = expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null)
        {
            Console.WriteLine("Expense not found.");
            return;
        }

        expenses.Remove(expense);
        SaveExpenses(expenses);

        Console.WriteLine("Expense deleted successfully");
    }

    static List<Expense> LoadExpenses()
    {
        return JsonConvert.DeserializeObject<List<Expense>>(File.ReadAllText(DataFilePath));
    }

    static void SaveExpenses(List<Expense> expenses)
    {
        File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(expenses, Formatting.Indented));
    }

    class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
