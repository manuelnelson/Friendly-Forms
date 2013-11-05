using System;
using System.Collections.Generic;
using System.Net;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using FriendlyForms.Recurring;
using Models.Helper;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class PaymentRestService
    {
        [Route("/Payments/oneTime", "POST")]
        public class PaymentDto : IReturn<PaymentDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public int AmountId { get; set; }
            public string CardNum { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string FullName { get; set; }
            public int ZipCode { get; set; }
            public int CvCode { get; set; }
        }

        [Route("/Payments/recurring", "POST")]
        public class RecurringPaymentDto : IReturn<RecurringPaymentDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public string CardNum { get; set; }
            public int ExpMonth { get; set; }
            public int ExpYear { get; set; }
            public string FullName { get; set; }
            public int AmountId { get; set; }
            public int ZipCode { get; set; }
            public int CvCode { get; set; }
        }

        public class PaymentsService : ServiceBase
        {
            //public IPaymentService PaymentService { get; set; } //Injected by IOC
            public IUserService UserService { get; set; }
            private const string UserName = "spli8523";
            private const string Password = "091BL6rX";
            private const string Vendor = "7866";
            private const string TransactionType = "Sale";//"Sale";

            [Authenticate]
            public void Post(PaymentDto request)
            {
                var transact = new Transaction.SmartPaymentsSoapClient("SmartPaymentsSoap");
                var date = request.ExpMonth.ToString().PrependZero() + request.ExpYear.ToString().PrependZero();
                var amount = request.AmountId == 1 ? "150" : "250";                
                var response = transact.ProcessCreditCard(UserName, Password, TransactionType, request.CardNum, date, "",
                                           request.FullName, amount, "", "", "", "", "", "");
                if (response.RespMSG == "Approved")
                //if (response.RespMSG == "Decline")
                {
                    //update Paid field
                    var user = UserService.Get(Convert.ToInt64(UserSession.CustomId));
                    user.Paid = true;
                    UserSession.Paid = true;
                    UserService.Update(user);
                    return;
                }
                throw new HttpError(HttpStatusCode.BadRequest, response.RespMSG);
            }

            [Authenticate]
            public void Post(RecurringPaymentDto request)
            {                
                var user = UserService.Get(Convert.ToInt64(UserSession.CustomId));
                var recurringSoapClient = new RecurringSoapClient("RecurringSoap");
                var date = request.ExpMonth.ToString().PrependZero() + request.ExpYear.ToString().PrependZero();
                var amount = GetAmount(request.AmountId);
                var customerResponse = recurringSoapClient.ManageCustomer(UserName, Password, "ADD", Vendor, "",
                                                                          user.Id.ToString(), request.FullName, "", "",
                                                                          "", "", "", "", "", "", "", "",
                                                                          request.ZipCode.ToString(), "", "", "", "", "", "", "ACTIVE","");
                if (customerResponse.code == ResultCode.OK)
                {
                    var response = recurringSoapClient.ManageCreditCardInfo(UserName, Password, "ADD", Vendor, customerResponse.CustomerKey, "",
                                                                            request.CardNum, date, request.FullName, "",
                                                                            request.ZipCode.ToString(), "");
                    user.CcInfoKey = response.CcInfoKey;
                    user.CustomerKey = customerResponse.CustomerKey;
                    user.RecurringDateStart = DateTime.UtcNow;
                    user.Paid = true;
                    UserSession.Paid = true;
                    UserService.Update(user);
                    response= recurringSoapClient.ProcessCreditCard(UserName, Password, Vendor, user.CcInfoKey, amount, "", "");
                    if(response.code == ResultCode.OK)
                        return;
                    throw new HttpError(HttpStatusCode.BadRequest, response.error);
                }
                throw new HttpError(HttpStatusCode.BadRequest, customerResponse.error);
            }
            private string GetAmount(int amountId)
            {
                switch (amountId)
                {
                    case (int)PaymentOptions.Silver:
                        return "315";
                    case (int)PaymentOptions.Gold:
                        return "500";
                    case (int)PaymentOptions.Premiere:
                        return "750";
                    default:
                        return "315";
                }
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
