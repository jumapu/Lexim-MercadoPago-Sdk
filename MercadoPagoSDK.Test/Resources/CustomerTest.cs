﻿using MercadoPago;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture()]
    public class CustomerTest
    {
        [Test()]
        public void Customer_CreateCustomerGetsCreatedCustomerInResponse()
        {
            MPConf.CleanConfiguration();
            MPConf.SetBaseUrl("https://api.mercadopago.com");
            MPConf.AccessToken = "TEST-4205497482754834-092513-34a1c5f06438b3a488bad9420cfe84e5__LB_LD__-261220529";

            Customer newCustomer = new Customer { first_name = "Rafa", last_name = "Williner" };
            Customer responseCustomer = newCustomer.Create();

            Assert.AreEqual(201, responseCustomer.GetLastApiResponse().StatusCode);
            Assert.AreEqual(newCustomer.first_name, responseCustomer.first_name);
            Assert.AreEqual(newCustomer.last_name, responseCustomer.last_name);
        }

        [Test()]
        public void Customer_CreateCustomerAndThenLoadGetsCreatedCustomer()
        {
            MPConf.CleanConfiguration();
            MPConf.SetBaseUrl("https://api.mercadopago.com");
            MPConf.AccessToken = "TEST-4205497482754834-092513-34a1c5f06438b3a488bad9420cfe84e5__LB_LD__-261220529";

            Customer newCustomer = new Customer { first_name = "Juan", last_name = "Perez" };
            Customer responseCustomer = newCustomer.Create();

            Customer loadedCustomer1 = Customer.Load(responseCustomer.id);
            Customer loadedCustomer2 = Customer.Load(responseCustomer.id, false);

            Assert.AreEqual(200, loadedCustomer1.GetLastApiResponse().StatusCode);
            Assert.AreEqual(200, loadedCustomer2.GetLastApiResponse().StatusCode);
            Assert.AreEqual(loadedCustomer1.first_name, newCustomer.first_name);
            Assert.AreEqual(loadedCustomer1.last_name, newCustomer.last_name);
            Assert.AreEqual(loadedCustomer2.first_name, newCustomer.first_name);
            Assert.AreEqual(loadedCustomer2.last_name, newCustomer.last_name);
        }

        [Test()]
        public void Customer_CreateCustomerAndThenUpdateUpdatesCustomer()
        {
            MPConf.CleanConfiguration();
            MPConf.SetBaseUrl("https://api.mercadopago.com");
            MPConf.AccessToken = "TEST-4205497482754834-092513-34a1c5f06438b3a488bad9420cfe84e5__LB_LD__-261220529";

            Customer newCustomer = new Customer { first_name = "Jorge", last_name = "Calciati" };
            Customer responseCustomer = newCustomer.Create();

            responseCustomer.last_name = "Calciati Rodriguez";
            responseCustomer.Update();

            Assert.AreEqual(200, responseCustomer.GetLastApiResponse().StatusCode);
            Assert.AreEqual("Calciati Rodriguez", responseCustomer.last_name);
        }

        [Test()]
        public void Customer_CreateCustomerAndThenDeleteDeletesCustomer()
        {
            MPConf.CleanConfiguration();
            MPConf.SetBaseUrl("https://api.mercadopago.com");
            MPConf.AccessToken = "TEST-4205497482754834-092513-34a1c5f06438b3a488bad9420cfe84e5__LB_LD__-261220529";

            Customer newCustomer = new Customer { first_name = "Pedro", last_name = "Juarez" };
            Customer responseCustomer = newCustomer.Create();

            string id = responseCustomer.id;

            responseCustomer.Delete();

            Customer nonExistingCustomer = Customer.Load(responseCustomer.id, false);

            Assert.AreEqual(404, nonExistingCustomer.GetLastApiResponse().StatusCode);
        }

        [Test()]
        public void Customer_SearchCustomersReturnListOfCustomers()
        {
            MPConf.CleanConfiguration();
            MPConf.SetBaseUrl("https://api.mercadopago.com");
            MPConf.AccessToken = "TEST-4205497482754834-092513-34a1c5f06438b3a488bad9420cfe84e5__LB_LD__-261220529";

            List<Customer> customers = Customer.Search();


            Assert.IsTrue(customers.Any());
            Assert.IsNotNull(customers.First());
            Assert.IsTrue(customers.First() is Customer);
        }
    }
}
