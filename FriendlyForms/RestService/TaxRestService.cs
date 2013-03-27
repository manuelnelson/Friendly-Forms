using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Taxes/")]
    public class ReqTax
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
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

    public class TaxRestService : Service
    {
        public ITaxService TaxService { get; set; }
        public object Get(ReqTax request)
        {
            if (request.Id != 0)
            {
                return TaxService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return TaxService.GetByUserId(request.UserId);
            }
            return new Tax();
        }
        public object Post(ReqTax request)
        {
            var tax = request.TranslateTo<Tax>();
            TaxService.Add(tax);
            return new RespTax()
                {
                    Id = tax.Id
                };
        }
        public object Put(ReqTax request)
        {
            var tax = request.TranslateTo<Tax>();
            TaxService.Update(tax);
            return new RespTax();
        }
    }
}