using Stripe.Net.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Net.Test
{
    public class Test
    {
        public static void Main(string[] args)
        {
            try {
                string apiKey = "sk_test_LrpNbKfRm7mXCjSzYbSl2FE1";

                var stripe = new StripeService(apiKey);

                stripe.CreateCustomerAsync("test1@example.com", "test 2").Wait();

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
