namespace Stripe.Net.Charges
{
    public enum CardFailureType
    {
        InvalidNumber,
        InvalidExpireMonth,
        InvalidExpireYear,
        InvalidCvc,

        ExpiredCard,
        IncorrectNumber,
        IncorrectCvc,
        CardDeclined,
        ProcessingError
    }
}
