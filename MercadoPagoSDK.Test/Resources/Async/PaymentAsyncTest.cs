#if NETCOREAPP2_1

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using MercadoPago.Resources;
using MercadoPago.DataStructures.Payment;
using MercadoPago;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;
using MercadoPago.Common;

namespace MercadoPagoSDK.Test.Resources
{
    [TestFixture] 
    public class PaymentAsyncTest
    {
        string PublicKey;
        Payment LastPayment; 

        [SetUp]
        public void Init()
        { 
            Authentication.Initialize(useAccessToken: true, useClientCredentials: false);
            PublicKey = Environment.GetEnvironmentVariable("PUBLIC_KEY");
        }

        [Test]
        public async Task Payment_Create_ShouldBeOk()
        {
            var addInfPayerAdd = new Address
            {
                StreetName = "aaa",
                StreetNumber = 5,
                ZipCode = "54321"
            };

            var addInfPayerPhone = new Phone
            {
                AreaCode = "00",
                Number = "5512345678"
            };

            DateTime fechaReg = new DateTime(2000, 01, 31);

            var addInfoPayer = new AdditionalInfoPayer
            {
                FirstName = "Rubén",
                LastName = "González",
                RegistrationDate = fechaReg,
                Address = addInfPayerAdd,
                Phone = addInfPayerPhone
            };

            var item = new Item
            {
                Id = "producto123",
                Title = "Celular blanco",
                Description = "4G, 32 GB",
                Quantity = 1,
                PictureUrl = "http://www.imagenes.com/celular.jpg",
                UnitPrice = 100.4m
            };


            List<Item> items = new List<Item>();
            items.Add(item);

            ReceiverAddress receiverAddress = new ReceiverAddress
            {
                StreetName = "insurgentes sur",
                StreetNumber = 1,
                Zip_code = "12345"
            };

            Shipment shipment = new Shipment
            {
                ReceiverAddress = receiverAddress
            };

            var addInf = new AdditionalInfo
            {
                Payer = addInfoPayer,
                Shipments = shipment,
                Items = items

            };
            
            Payment payment = new Payment
            {
                TransactionAmount = 20.0m,
                Token = Helpers.CardHelper.SingleUseCardToken(PublicKey, "pending"), // 1 use card token
                Description = "Pago de Prueba",
                PaymentMethodId = "visa",
                ExternalReference = "INTEGRATION-TEST-PAYMENT",
                Installments = 1,
                Payer = new Payer {
                    Email = "milton.brandes@mercadolibre.com"
                },
                AdditionalInfo = addInf
            };

            await payment.SaveAsync(); 
             
            LastPayment = payment;
 
 
            Assert.IsTrue(payment.Id.HasValue, "Failed: Payment could not be successfully created");
            Assert.IsTrue(payment.Id.Value > 0, "Failed: Payment has not a valid id"); 
        }

        [Test]
        public async Task Payment_FindById_ShouldBeOk()
        { 
            Payment payment = await Payment.FindByIdAsync(LastPayment.Id); 
            Assert.AreEqual("Pago de Prueba", payment.Description); 
        }

        [Test]
        [Ignore("Test is failing even in official SDK due to invalid additional_info property on PUT request.")]
        public async Task Payment_Update_ShouldBeOk() 
        {  
            LastPayment.Status = PaymentStatus.cancelled;
            await LastPayment.UpdateAsync();

            Assert.AreEqual(PaymentStatus.cancelled, LastPayment.Status); 
        }

        [Test] 
        public async Task Payment_Refund()
        {
            
            Payment OtherPayment = new Payment
            {
                TransactionAmount = 10.0m,
                Token = Helpers.CardHelper.SingleUseCardToken(PublicKey, "approved"), // 1 use card token
                Description = "Pago de Prueba",
                PaymentMethodId = "visa",
                ExternalReference = "REFUND-TEST-PAYMENT",
                Installments = 1,
                Payer = new Payer {
                    Email = "milton.brandes@mercadolibre.com"
                }
            };

            await OtherPayment.SaveAsync();
            await OtherPayment.RefundAsync(); 

            Assert.AreEqual(PaymentStatus.refunded, OtherPayment.Status, "Failed: Payment could not be successfully refunded");
        }

    }
}

#endif