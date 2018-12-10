using MercadoPago;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using MercadoPago.DataStructures.Customer;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture]
    public class CustomerTest
    {
        private const string Email = "Rafa.Williner@gmail.com";
        private const string LastName = "Calciati Rodriguez";
        private string _accessToken;
        private Customer _lastCustomer;

        [SetUp]
        public void Init()
        {
            // Avoid SSL Cert error
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            // HardCoding Credentials
            _accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            // Make a Clean Test
            SDK.CleanConfiguration();
            SDK.SetBaseUrl("https://api.mercadopago.com");
            SDK.AccessToken = _accessToken;
        }

        [Test, Order(1)]
        public void Customer_Create_ShouldBeOk()
        {
            var customer = new Customer
            {
                FirstName = "Rafa",
                LastName = "Williner",
                Email = Email,
                Address = new Address
                {
                    StreetName = "some street",
                    ZipCode = "2300"
                },
                Phone = new Phone
                {
                    AreaCode = "03492",
                    Number = "432334"
                },
                Description = "customer description",
                Identification = new Identification
                {
                    Type = "DNI",
                    Number = "29804555"
                }
            };

            customer.Save();
            _lastCustomer = customer;

            Assert.IsTrue(customer.Id != null, $"Failed: Customer could not be successfully created.");

            Console.WriteLine($"id: {customer.Id}");
        }

        [Test, Order(2)]
        public void Customer_FindById_ShouldBeOk()
        {
            Customer customer = Customer.FindById(_lastCustomer.Id);
            Assert.AreEqual(customer.FirstName, _lastCustomer.FirstName);
        }

        [Test, Order(3)]
        public void Customer_Update_ShouldBeOk()
        {
            _lastCustomer.LastName = LastName;
            _lastCustomer.Update();

            Assert.AreEqual(_lastCustomer.LastName, LastName);
        }

        [Test, Order(4)]
        public void Customer_SearchWithFilterGetListOfCustomers()
        {
            Thread.Sleep(1000);
            var filters = new Dictionary<string, string>
            {
                {"email", Email}
            };

            var customers = Customer.Search(filters);

            Assert.IsTrue(customers.Any());
            Assert.IsNotNull(customers.First());
            Assert.AreEqual(customers.First().Email, Email);
        }

        [Test, Order(5)]
        public void Customer_LinqQueryByEmail()
        {
            var customers =
                Customer.Query()
                        .Where(x => x.Email == Email)
                        .ToList();

            Assert.IsTrue(customers.Any());
            Assert.IsNotNull(customers.First());
            Assert.AreEqual(customers.First().Email, Email);
        }

        [Test, Order(6)]
        public void Customer_LinqQueryByLastName()
        {
            var customers =
                Customer.Query()
                        .Where(x => x.LastName == LastName)
                        .ToList();

            Assert.IsTrue(customers.Any());
            Assert.IsNotNull(customers.First());
            Assert.AreEqual(customers.First().Email, Email);
        }

        [Test, Order(7)]
        public void Remove_Customer()
        {
            _lastCustomer.Delete();
        }
    }
}
