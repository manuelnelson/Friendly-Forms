using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Debt/")]
    public class ReqDebt
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int MaritalDebt { get; set; }
        [DataMember]
        public string DebtDivision { get; set; }
    }

    [DataContract]
    public class RespDebt : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class DebtRestService : Service
    {
        public IDebtService DebtService { get; set; }

        public object Post(ReqDebt request)
        {
            var debtViewModel = request.TranslateTo<DebtViewModel>();
            DebtService.AddOrUpdate(debtViewModel);
            return new RespDebt();
        }
    }
}