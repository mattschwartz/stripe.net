using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestStripeNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try {
                StripeConfiguration.SetApiKey("sk_test_LrpNbKfRm7mXCjSzYbSl2FE1");

                var newCharge = new StripeChargeCreateOptions
                {
                    Amount = 500,
                    Currency = "usd",
                    Description = "Testing charges from new library..." ,
                    CustomerId = "cus_9tvLjBM1FynkwG",
                    SourceTokenOrExistingSourceId = "card_19a82RJB5O7unlMsd398UwIG"
                };

                var chargeService = new StripeChargeService();
                var charge = chargeService.Create(newCharge);

            } catch (Exception ex) {
                Console.WriteLine("[{0}] Unexpected exception: {1}\nInner Message: {2}\nStack Trace: {3}",
                    DateTime.Now.ToString("HH:mm:ss yyyy/MM/dd"),
                    ex.Message,
                    ex.InnerException?.Message ?? "None",
                    ex.StackTrace);
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
