using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            HouseholdAccounts account = new HouseholdAccounts();
            account.AddExpense();
            account.Show();
            account.Search();
            account.Delete();
            account.Modify();
            account.Sort();
            account.Normalize();


        }
    }
}
