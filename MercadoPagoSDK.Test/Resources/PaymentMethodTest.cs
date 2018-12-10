using System;
using System.Collections.Generic;
using System.Net;
using MercadoPago;
using MercadoPago.Resources;
using NUnit.Framework;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture] 
    public class PaymentMethodTest
    {
        [SetUp]
        public void Init() => Authentication.Initialize(useAccessToken: true, useClientCredentials: false);

        [Test]
        public void PaymentMethod_All_ShouldReturnValues()
        {
            List<PaymentMethod> paymentMethods = PaymentMethod.All(); 

            Assert.IsTrue(paymentMethods.Count > 1,  "Failed: Can't get payment methods");

        }

    }
}
