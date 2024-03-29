﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stripe.Net.Cards;
using Stripe.Net.BankPaymentAccounts;

namespace Stripe.Net.Customers
{
    public class Customer
    {
        [JsonProperty("created")]
        public long _created { private get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account_balance")]
        public decimal AccountBalance { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        [JsonProperty("sources")]
        public dynamic _sources { private get; set; }

        public List<StripePaymentMethod> Sources
        {
            get
            {
                var result = new List<StripePaymentMethod>();

                foreach (var source in _sources.data) {
                    if (source["object"] == "card") {
                        var card = new Card
                        {
                            Id = source.id,
                            Brand = source.brand,
                            ExpirationMonth = (int) source.exp_month,
                            ExpirationYear = (int) source.exp_year,
                            Fingerprint = source.fingerprint,
                            LastFour = source.last4,
                            _cvcCheck = source.cvc_check
                        };
                        result.Add(card);
                    } else if (source["object"] == "bank_account") {
                        var bankAccount = new BankPaymentAccount
                        {
                            Id = source.id,
                            Account = source.account,
                            BankName = source.bank_name,
                            AccountHolderName = source.account_holder_name,
                            RoutingNumber = source.routing_number,
                            Fingerprint = source.fingerprint,
                            LastFour = source.last4,
                            _status = source.status
                        };

                        result.Add(bankAccount);
                    }
                }

                return result;
            }
        }

        public DateTime Created
        {
            get
            {
                var unixTime = new DateTime(1970, 1, 1);
                return unixTime.AddSeconds(_created);
            }
        }
    }
}
