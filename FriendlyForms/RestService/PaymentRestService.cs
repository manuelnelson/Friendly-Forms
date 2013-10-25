﻿using System;
using System.Collections.Generic;
using System.Net;
using FriendlyForms.Helpers;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class PaymentRestService
    {
        [Route("/Payments", "POST")]
        [Route("/Payments", "PUT")]
        [Route("/Payments", "GET")]
        [Route("/Payments", "DELETE")]
        [Route("/Payments")]
        [Route("/Payments/{Id}")]
        public class PaymentDto : IReturn<PaymentDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public string CardNum { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string FullName { get; set; }
            public double Amount { get; set; }
            public int ZipCode { get; set; }
            public int CvCode { get; set; }
        }

        public class PaymentsService : Service
        {
            //public IPaymentService PaymentService { get; set; } //Injected by IOC

            //public object Get(PaymentDto request)
            //{
            //    if (request.Ids != null && request.Ids.Length > 0)
            //        return PaymentService.Get(request.Ids);
            //    if (request.Id > 0)
            //        return PaymentService.Get(request.Id);
            //    throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            //}

            //public object Put(PaymentDto request)
            //{
            //    var PaymentEntity = request.TranslateTo<Payment>();
            //    PaymentService.Update(PaymentEntity);
            //    return PaymentEntity;
            //}
            private const string UserName = "spli8523";
            private const string Password = "091BL6rX";
            private const string TransactionType = "Sale";//"Sale";
            public void Post(PaymentDto request)
            {
                var transact = new Transaction.SmartPaymentsSoapClient("SmartPaymentsSoap");
                var date = request.ExpMonth.ToString().PrependZero() + request.ExpYear.ToString().PrependZero();
                //var response = transact.ProcessCreditCard(UserName, Password, TransactionType, request.CardNum, date, null,
                //                           request.FullName, request.Amount.ToString(), null, null, null, null,
                //                           null, null);
                var response = transact.ProcessCreditCard(UserName, Password, TransactionType, request.CardNum, date, "",
                                           request.FullName, request.Amount.ToString(), "", "", "", "", "", "");
                //transact.ProcessCreditCard()
                Console.Write(response);                
            }

            //public void Delete(PaymentDto request)
            //{
            //    if (request.Ids != null && request.Ids.Length > 0)
            //        PaymentService.DeleteAll(request.Ids);
            //    else
            //        PaymentService.Delete(request.Id);
            //}
        }
    }

}