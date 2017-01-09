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

                stripe.AddBankAccountAsync("cus_9slVwRKZeS6dxa", "testing1", AccountHolderType.Company,
                    "000123456789", "110000000").Wait();

                Customer customer = stripe.GetCustomerAsync("cus_9slVwRKZeS6dxa").Result;

                Console.WriteLine("Found customer {0}, {1}, {2}, {3}",
                    customer.Id, customer.Email, customer.Description,
                    customer.Created);
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
