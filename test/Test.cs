using Stripe.Net.Cards;
using Stripe.Net.Customers;
using System;

namespace Stripe.Net.Test
{
    class TestFailedException : Exception
    {
        public TestFailedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }
    }

    public class Test
    {
        private const string API_KEY = "sk_test_LrpNbKfRm7mXCjSzYbSl2FE1";
        private const string SAMPLE_CUSTOMER_EMAIL = "testing@example.com";
        public static string Timestamp
        {
            get
            {
                return DateTime.Now.ToString("HH:mm:ss yyyy/MM/dd");
            }
        }

        public static void Main(string[] args)
        {
            try {
                Test_CreateCustomer();
                Test_CreateDuplicateCustomer();
                Test_FetchCustomer();

                Test_AddCardToCustomer();
                Test_FetchCard();
                Test_UpdateCard();
                Test_DeleteCard();
            } catch (TestFailedException ex) {
                Console.WriteLine("[{0}]: {1}", Timestamp, ex.Message);
            } catch (Exception ex) {
                Console.WriteLine("[{0}] Unexpected exception: {1}\nInner Message: {2}\nStack Trace: {3}",
                    Timestamp,
                    ex.Message,
                    ex.InnerException?.Message ?? "None",
                    ex.StackTrace);
            }

            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        private static string _testCustomerId;
        private static string _testCardId;
        private static string _testBankAccountId;

        private static void Test_CreateCustomer()
        {
            Console.Write("[{0}] Testing create customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            Customer customer = stripe.CreateCustomerAsync(SAMPLE_CUSTOMER_EMAIL, "A sample customer.").Result;

            if (customer == null) {
                Console.WriteLine();
                if (!stripe.HasError) {
                    throw new TestFailedException("Customer creation failed for unknown reasons.");
                }

                throw new TestFailedException("Customer creation failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            _testCustomerId = customer.Id;

            Console.WriteLine("pass");
        }

        private static void Test_CreateDuplicateCustomer()
        {
            Console.Write("[{0}] Testing create customer with existing email... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            Customer customer = stripe.CreateCustomerAsync(SAMPLE_CUSTOMER_EMAIL, "A sample customer.").Result;

            if (customer != null) {
                Console.WriteLine();
                throw new TestFailedException("Customer was created and should not have been.");
            }

            Console.WriteLine("pass");
        }

        private static void Test_FetchCustomer()
        {
            Console.Write("[{0}] Testing fetch customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            Customer customer = stripe.GetCustomerAsync(_testCustomerId).Result;

            if (customer == null) {
                Console.WriteLine();
                if (!stripe.HasError) {
                    throw new TestFailedException("Fetch customer failed for unknown reasons.");
                }

                throw new TestFailedException("Fetch customer failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            if (customer.Email != SAMPLE_CUSTOMER_EMAIL) {
                throw new TestFailedException("Fetched customer has different email.");
            }

            Console.WriteLine("pass");
        }

        private static void Test_AddCardToCustomer()
        {
            Console.Write("[{0}] Testing add card to customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            Card card = stripe.AddCardAsync(
                _testCustomerId,
                7,
                DateTime.Now.AddYears(3).Year,
                123,
                "4242424242424242").Result;

            if (card == null) {
                Console.WriteLine();
                if (!stripe.HasError) {
                    throw new TestFailedException("Add card failed for unknown reasons.");
                }

                throw new TestFailedException("Add card failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            _testCardId = card.Id;

            Console.WriteLine("pass");
        }

        private static void Test_FetchCard()
        {
            Console.Write("[{0}] Testing fetch card from customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            Card card = stripe.GetCardAsync(_testCustomerId, _testCardId).Result;

            if (card == null) {
                Console.WriteLine();
                if (!stripe.HasError) {
                    throw new TestFailedException("Fetch card failed for unknown reasons.");
                }

                throw new TestFailedException("Fetch card failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            Console.WriteLine("pass");
        }

        private static void Test_UpdateCard()
        {
            Console.Write("[{0}] Testing update card on customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);
            string newCity = "Testing City";
            string newLineOne = "123 Address St";
            string newZip = "78729";

            Card card = stripe.UpdateCardAsync(
                _testCustomerId,
                _testCardId,
                city: newCity,
                lineOne: newLineOne,
                zip: newZip).Result;

            if (card == null) {
                Console.WriteLine();
                if (!stripe.HasError) {
                    throw new TestFailedException("Update card failed for unknown reasons.");
                }

                throw new TestFailedException("Update card failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            if (card.AddressZip != newZip) {
                throw new TestFailedException("Update card failed: zip is not equal");
            }
            if (card.AddressCity != newCity) {
                throw new TestFailedException("Update card failed: city is not equal");
            }
            if (card.AddressLineOne != newLineOne) {
                throw new TestFailedException("Update card failed: line1 is not equal");
            }

            Console.WriteLine("pass");
        }

        private static void Test_DeleteCard()
        {
            Console.Write("[{0}] Testing delete card from customer... ", Timestamp);
            var stripe = new StripeService(API_KEY);

            stripe.DeleteCardAsync(_testCustomerId, _testCardId).Wait();

            if (stripe.HasError) {
                Console.WriteLine();
                throw new TestFailedException("Delete card failed ({0}): {1} {2}",
                    stripe.Error.Type,
                    stripe.Error.Message,
                    stripe.Error.Parameter);
            }

            Console.WriteLine("pass");
        }
    }
}