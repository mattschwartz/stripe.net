namespace Stripe.Net.Cards
{
    public class Card : StripePaymentMethod
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public CvcCheckStatus CvcCheck { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string LastFour { get; set; }
        public string Fingerprint { get; set; }
    }
}
