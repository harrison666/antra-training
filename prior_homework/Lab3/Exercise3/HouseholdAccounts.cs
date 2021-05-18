using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Exercise3
{
    class Expense
    {
        public Expense(DateTime date, string description, string category, float amount)
        {
            Date = date;
            Description = description;
            Category = category;
            Amount = amount;
        }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Amount { get; set; }

    }
    class HouseholdAccounts
    {
        List<Expense> expenses = new List<Expense>();

        public void AddExpense()
        {
            Console.WriteLine("Enter Date(yyyy-MM-dd format): ");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            Console.WriteLine("Enter Description: ");
            string description = Console.ReadLine();
            while (description == "")
            {
                Console.WriteLine("Description cannot be empty, please enter again: ");
                description = Console.ReadLine();
            }

            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();

            Console.WriteLine("Enter Amount: ");
            float amount = (float)Convert.ToDouble(Console.ReadLine());

            Expense e = new Expense(date, description, category, amount);
            expenses.Add(e);
        }

        public void Show()
        {
            Console.WriteLine("Enter Category you want to show: ");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Date From(yyyy-MM-dd format): ");
            DateTime dateFrom = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Date To(yyyy-MM-dd format): ");
            DateTime dateTo = DateTime.Parse(Console.ReadLine());

            int length = expenses.Count;
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                if (expenses[i].Category == category && expenses[i].Date >= dateFrom && expenses[i].Date <= dateTo)
                {
                    count += 1;
                    Console.WriteLine($"{i}-{expenses[i].Date.ToString("dd/MM/yyyy")}-{expenses[i].Description}-({expenses[i].Category})-{Math.Round(expenses[i].Amount, 2)}");
                }
            }
            Console.WriteLine("Total amount of data displayed: " + count);
        }

        public void Search()
        {
            Console.WriteLine("Enter a certain text you want to search in costs: ");
            string txt = Console.ReadLine();

            int length = expenses.Count;
            for (int i = 0; i < length; i++)
            {
                if (expenses[i].Amount < 0 && (expenses[i].Description.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0
                    || expenses[i].Category.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    Console.WriteLine($"{i}-{expenses[i].Date.ToString("dd/MM/yyyy")}-{expenses[i].Description.Substring(0, 6)}");
                }
            }
        }

        public void Modify()
        {
            Console.WriteLine("Enter tab number you want to modify: ");
            int i = Convert.ToInt32(Console.ReadLine());
            while (i >= expenses.Count)
            {
                Console.WriteLine("Invalid tab number, please enter again: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine($"{i}-{expenses[i].Date.ToString("dd/MM/yyyy")}-{expenses[i].Description}-({expenses[i].Category})-{Math.Round(expenses[i].Amount, 2)}");
            Console.ReadKey();


            Console.WriteLine("Enter Date(yyyy-MM-dd format): ");
            DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            Console.WriteLine("Enter Description: ");
            string description = Console.ReadLine();
            while (description == "")
            {
                Console.WriteLine("Description cannot be empty, please enter again: ");
                description = Console.ReadLine();
            }

            Console.WriteLine("Enter Category: ");
            string category = Console.ReadLine();

            Console.WriteLine("Enter Amount: ");
            float amount = (float)Convert.ToDouble(Console.ReadLine());

            expenses[i].Date = date;
            expenses[i].Description = description;
            expenses[i].Category = category;
            expenses[i].Amount = amount;

        }

        public void Delete()
        {
            Console.WriteLine("Enter tab number you want to delete: ");
            int i = Convert.ToInt32(Console.ReadLine());
            while (i >= expenses.Count)
            {
                Console.WriteLine("Invalid tab number, please enter again: ");
                i = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine($"{i}-{expenses[i].Date.ToString("dd/MM/yyyy")}-{expenses[i].Description}-({expenses[i].Category})-{Math.Round(expenses[i].Amount, 2)}");
            expenses[i] = null;
        }

        public void Sort()
        {
            expenses.OrderBy(x => x.Date).ThenBy(x => x.Description);
        }

        public void Normalize()
        {
            int length = expenses.Count;
            for (int i = 0; i < length; i++)
            {
                expenses[i].Description.Trim();
                
                if (expenses[i].Description.All(Char.IsUpper))
                {

                    string[] des = expenses[i].Description.Split(" ");
                    for (int j = 0; j < des.Length; j++)
                    {

                    }
                }
                expenses[i].Description.Replace(" ", "");
                
            }
        }
    }

}
