using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Net.BankAccounts
{
    public class BankAccount : StripePaymentMethod
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string FingerPrint { get; set; }
        public string LastFour { get; set; }
        public string RoutingNumber { get; set; }
        public BankAccountStatus Status { get; set; }
    }
}
