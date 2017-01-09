using System.ComponentModel;

namespace Stripe.Net
{
    public enum BankAccountStatus
    {
        [Description("new")]
        New = 0,
        [Description("validated")]
        Validated = 1,
        [Description("verified")]
        Verified = 2,
        [Description("verification_failed")]
        VerificationFailed = 3,
        [Description("errored")]
        Errored = 4
    }
}
