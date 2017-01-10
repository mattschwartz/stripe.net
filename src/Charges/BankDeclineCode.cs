using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Net.Charges
{
    public class BankDeclineCode
    {
        /// <summary>
        /// The payment cannot be authorized
        /// Next steps: The payment should be attempted again. If it still cannot be processed, the customer needs to contact their bank.
        /// </summary>
        public const string ApproveWithId = "approve_with_id";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string CallIssuer = "call_issuer";
        /// <summary>
        /// The card does not support this type of purchase
        /// Next steps: The customer needs to contact their bank to make sure their card can be used to make this type of purchase.
        /// </summary>
        public const string CardNotSupported = "card_not_supported";
        /// <summary>
        /// The customer has exceeded the balance or credit limit available on their card.
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string CardVelocityExceeded = "card_velocity_exceeded";
        /// <summary>
        /// The card does not support the specified currency.
        /// Next steps: The customer needs check with the issuer that the card can be used for the type of currency specified.
        /// </summary>
        public const string CurrencyNotSupported = "currency_not_supported";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string DoNotHonor = "do_not_honor";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string DoNotTryAgain = "do_not_try_again";
        /// <summary>
        /// The payment has been declined as the issuer suspects it is fraudulent
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string Fraudulent = "fraudulent";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string GenericDecline = "generic_decline";
        /// <summary>
        /// The card has insufficient funds to complete the purchase
        /// Next steps: The customer should use an alternative payment method.
        /// </summary>
        public const string InsufficientFunds = "insufficient_funds";
        /// <summary>
        /// The card, or account the card is connected to, is invalid
        /// Next steps: The customer needs to contact their bank to check that the card is working correctly.
        /// </summary>
        public const string InvalidAccount = "invalid_account";
        /// <summary>
        /// The payment amount is invalid, or exceeds the amount that is allowed
        /// Next steps: If the amount appears to be correct, the customer needs to check with their bank that they can make purchases of that amount.
        /// </summary>
        public const string InvalidAmount = "invalid_amount";
        /// <summary>
        /// The PIN entered is incorrect. This decline code only applies to payments made with a card reader.
        /// Next steps: The customer should try again using the correct PIN.
        /// </summary>
        public const string InvalidPin = "invalid_pin";
        /// <summary>
        /// The card issuer could not be reached, so the payment could not be authorized
        /// Next steps: The payment should be attempted again. If it still cannot be processed, the customer needs to contact their bank.
        /// </summary>
        public const string IssuerNotAvailable = "issuer_not_available";
        /// <summary>
        /// The payment has been declined because the card is reported lost
        /// Next steps: The specific reason for the decline should not be reported to the customer. Instead, it needs to be presented as a generic decline.
        /// </summary>
        public const string LostCard = "lost_card";
        /// <summary>
        /// The card, or account the card is connected to, is invalid
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string NewAccountInformationAvailable = "new_account_information_available";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string NoActionTaken = "no_action_taken";
        /// <summary>
        /// The payment is not permitted
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string NotPermitted = "not_permitted";
        /// <summary>
        /// The card cannot be used to make this payment (it is possible it has been reported lost or stolen)
        /// Next steps: 
        /// </summary>
        public const string PickupCard = "pickup_card";
        /// <summary>
        /// The payment could not be processed by the issuer for an unknown reason.
        /// Next steps: The payment should be attempted again. If it still cannot be processed, the customer needs to contact their bank.
        /// </summary>
        public const string Reenter_Transaction = "reenter_transaction";
        /// <summary>
        /// The card cannot be used to make this payment (it is possible it has been reported lost or stolen)
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string RestrictedCard = "restricted_card";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string RevocationOfAllAuthorizations = "revocation_of_all_authorizations";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string RevocationOfAuthorization = "revocation_of_authorization";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string SecurityViolation = "security_violation";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string ServiceNotAllowed = "service_not_allowed";
        /// <summary>
        /// The payment has been declined because the card is reported stolen
        /// Next steps: The specific reason for the decline should not be reported to the customer. Instead, it needs to be presented as a generic decline.
        /// </summary>
        public const string StolenCard = "stolen_card";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer should contact their bank for more information.
        /// </summary>
        public const string StopPaymentOrder = "stop_payment_order";
        /// <summary>
        /// A Stripe test card number was used
        /// Next steps: A genuine card must be used to make a payment.
        /// </summary>
        public const string TestModeDecline = "testmode_decline";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: The customer needs to contact their bank for more information.
        /// </summary>
        public const string TransactionNotAllowed = "transaction_not_allowed";
        /// <summary>
        /// The card has been declined for an unknown reason
        /// Next steps: Ask the customer to attempt the payment again. If subsequent payments are declined, the customer should contact their bank for more information.
        /// </summary>
        public const string TryAgainLater = "try_again_later";
        /// <summary>
        /// The customer has exceeded the balance or credit limit available on their card.
        /// Next steps: The customer should use an alternative payment method.
        /// </summary>
        public const string WithdrawalCountLimitExceeded = "withdrawal_count_limit_exceeded";
    }
}
