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
    [Route("/Support/")]
    public class ReqChildSupport
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string PaidBy { get; set; }
        [DataMember]
        public string PaidTo { get; set; }
        [DataMember]
        public string MonthlyAmount { get; set; }
        [DataMember]
        public string EffectiveDate { get; set; }
        [DataMember]
        public int TemporaryAgreement { get; set; }
        [DataMember]
        public int Payment { get; set; }
        [DataMember]
        public int? PaymentDay { get; set; }
        [DataMember]
        public string AdditionalAssets { get; set; }
    }

    [DataContract]
    public class RespChildSupport : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ChildSupportRestService : Service
    {
        public IChildSupportService ChildSupportService { get; set; }
        public object Get(ReqChildSupport request)
        {
            if (request.Id != 0)
            {
                return ChildSupportService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return ChildSupportService.GetByUserId(request.UserId);
            }
            return new ChildSupport();
        }
        public object Post(ReqChildSupport request)
        {
            var childSupport = request.TranslateTo<ChildSupport>();
            ChildSupportService.Add(childSupport);
            return new RespChildSupport();
        }
        public object Put(ReqChildSupport request)
        {
            var childSupport = request.TranslateTo<ChildSupport>();
            ChildSupportService.Update(childSupport);
            return new RespChildSupport();
        }
    }
}