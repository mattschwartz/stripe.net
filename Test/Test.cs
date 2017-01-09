using Stripe.Net.BankAccounts;
using Stripe.Net.Customers;
using System;

namespace Stripe.Net.Test
{
    public class Test
    {
        public static void Main(string[] args)
        {
            try {
                string apiKey = "sk_test_LrpNbKfRm7mXCjSzYbSl2FE1";

                var stripe = new StripeService(apiKey);

                //stripe.AddBankAccountAsync("cus_9slVwRKZeS6dxa", "testing1", AccountHolderType.Company, "000123456789", "110000000").Wait();

                BankAccount account = stripe.GetBankAccountAsync("cus_9slVwRKZeS6dxa", "ba_19a6isJB5O7unlMsvJHOR9K8").Result;
                Console.WriteLine("Bank account: {0}", account.AccountHolderName);

                account = stripe.UpdateBankAccountAsync("cus_9slVwRKZeS6dxa", "ba_19a6isJB5O7unlMsvJHOR9K8", accountHolderName: "TESTING 2").Result;
                Console.WriteLine("Bank account: {0}", account.AccountHolderName);
            } catch (Exception ex) {
                Console.WriteLine("Exception: {0}\nInner Message: {1}\nStack Trace: {2}",
                    ex.Message,
                    ex.InnerException?.Message ?? "None",
                    ex.StackTrace);
            }

            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }
}
