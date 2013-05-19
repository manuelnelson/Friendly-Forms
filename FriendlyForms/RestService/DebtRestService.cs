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
    [Route("/Debts/")]
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class DebtRestService : Service
    {
        public IDebtService DebtService { get; set; }
        public object Get(ReqDebt request)
        {
            if (request.Id != 0)
            {
                return DebtService.Get(request.Id);
            }
            if (request.UserId != 0)
            {
                return DebtService.GetByUserId(request.UserId);
            }
            return new Debt();
        }
        public object Post(ReqDebt request)
        {
            var debt = request.TranslateTo<Debt>();
            DebtService.Add(debt);
            return new RespDebt()
                {
                    Id = debt.Id
                };
        }
        public object Put(ReqDebt request)
        {
            var debt = request.TranslateTo<Debt>();
            DebtService.Update(debt);
            return new RespDebt();
        }
    }
}