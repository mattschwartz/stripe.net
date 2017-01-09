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

                //stripe.AddCardAsync(
                //    "cus_9slVwRKZeS6dxa",
                //    8,
                //    2019,
                //    123,
                //    "4242424242424242").Wait();

                stripe.GetCardAsync("cus_9slVwRKZeS6dxa", "card_19a4zPJB5O7unlMs4tvH3BEE").Wait();

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
