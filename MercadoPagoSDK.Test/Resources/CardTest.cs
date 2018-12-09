using MercadoPago;
using MercadoPago.Resources;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture]
    public class CardTest
    {
        private const string Email = "temp.customer@gmail.com";
        string AccessToken;
        string PublicKey;
        Customer _customer;
        Card _card;

        [SetUp]
        public void Init()
        {
            // Avoid SSL Cert error
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;
            // HardCoding Credentials
            AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            PublicKey = Environment.GetEnvironmentVariable("PUBLIC_KEY");
            // Make a Clean Test
            SDK.CleanConfiguration();
            SDK.SetBaseUrl("https://api.mercadopago.com");
            SDK.AccessToken = AccessToken;
        }

        [Test, Order(10)]
        public void EnsureCustomer()
        {
            _customer = null;
            _customer = 
                Customer.Query()
                        .FirstOrDefault(x => x.Email == Email);

            if (_customer == null)
            {
                _customer = new Customer
                {
                    Email = Email
                };
                _customer.Save();
            }
        }

        [Test, Order(20)]
        public void Card_ClearExistingCards()
        {
            var cards = Card.All(_customer.Id);

            foreach (var c in cards)
                c.Delete();

            cards = Card.All(_customer.Id);

            Assert.AreEqual(cards.Count, 0);
        }
        
        [Test, Order(30)]
        public void Card_CreateShouldBeOk()
        {
            Thread.Sleep(2000);

            _card = new Card
            {
                CustomerId = _customer.Id,
                Token = Helpers.CardHelper.SingleUseCardToken(PublicKey, "pending")
            };

            _card.Save();

            Assert.IsNotEmpty(_card.Id); 
        }

        [Test, Order(40)]
        public void Card_FindById_ShouldBeOk()
        {
            Thread.Sleep(1000);
            _card = Card.FindById(_customer.Id, _card.Id); 
            Console.WriteLine($"CardId: {_card.Id}");
            Assert.IsNotEmpty(_card.Id);  
        }
        
        [Test, Order(50)]
        //[Ignore("Endpoint is failing with status 400. Compare implementation with official SDK.")]
        public void Card_UpdateShouldBeOk()
        {
            var lastToken = _card.Token;
            _card.Token = Helpers.CardHelper.SingleUseCardToken(PublicKey, "not_founds"); 
            _card.Update();

            Assert.AreNotEqual(lastToken, _card.Token);
        }

        [Test, Order(60)]
        public void RemoveCard()
        {
            _card.Delete();
        }

        [Test, Order(70)]
        public void RemoveCustomer()
        {
            Thread.Sleep(2000);
            _customer.Delete();
        }
    }
}
