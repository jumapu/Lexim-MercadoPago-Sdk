using System;
using System.ComponentModel.DataAnnotations;
using MercadoPago.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MercadoPago.DataStructures.Preference
{
    public struct PaymentType
    {
        /// <summary>
        /// Payment method data_type ID
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PaymentTypeId? Id { get; set; }

        public static readonly PaymentType AccountMoney = new PaymentType {Id = PaymentTypeId.account_money };
        public static readonly PaymentType Ticket = new PaymentType { Id = PaymentTypeId.ticket };
        public static readonly PaymentType BankTransfer = new PaymentType { Id = PaymentTypeId.bank_transfer };
        public static readonly PaymentType ATM = new PaymentType { Id = PaymentTypeId.atm };
        public static readonly PaymentType CreditCard = new PaymentType { Id = PaymentTypeId.credit_card };
        public static readonly PaymentType DebitCard = new PaymentType { Id = PaymentTypeId.debit_card };
        public static readonly PaymentType PrepaidCard = new PaymentType { Id = PaymentTypeId.prepaid_card };
    }
}
