using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Tax/")]
    public class ReqTax
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Taxes { get; set; }
        [DataMember]
        public string TaxDescription { get; set; }
    }

    [DataContract]
    public class RespTax : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class TaxRestService : ServiceBase
    {
        public ITaxService TaxService { get; set; }
        public object Get(ReqTax request)
        {
            if (request.Id != 0)
            {
                return TaxService.Get(request.Id);
            }
            return TaxService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqTax request)
        {
            var tax = request.TranslateTo<Tax>();
            tax.UserId = Convert.ToInt32(UserSession.CustomId);
            TaxService.Add(tax);
            return new RespTax()
                {
                    Id = tax.Id
                };
        }
        public object Put(ReqTax request)
        {
            var tax = request.TranslateTo<Tax>();
            tax.UserId = Convert.ToInt32(UserSession.CustomId);
            TaxService.Update(tax);
            return new RespTax();
        }
    }
}