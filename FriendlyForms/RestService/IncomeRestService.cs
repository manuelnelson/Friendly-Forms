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
    [Route("/Income/")]
    public class ReqIncome
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public int HaveSalary { get; set; }
        [DataMember]
        public string OtherIncome { get; set; }
        [DataMember]
        public int? W2Income { get; set; }
        [DataMember]
        public int? NonW2Income { get; set; }
        [DataMember]
        public int? SelfIncome { get; set; }
        [DataMember]
        public int? SelfIncomeNoDeductions { get; set; }
        [DataMember]
        public string Commisions { get; set; }
        [DataMember]
        public string Bonuses { get; set; }
        [DataMember]
        public string Overtime { get; set; }
        [DataMember]
        public string Severance { get; set; }
        [DataMember]
        public string Retirement { get; set; }
        [DataMember]
        public string Interest { get; set; }
        [DataMember]
        public string Dividends { get; set; }
        [DataMember]
        public string Trust { get; set; }
        [DataMember]
        public string Annuities { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public string SocialSecurity { get; set; }
        [DataMember]
        public string Compensation { get; set; }
        [DataMember]
        public string Unemployment { get; set; }
        [DataMember]
        public string CivilCase { get; set; }
        [DataMember]
        public string Gifts { get; set; }
        [DataMember]
        public string Prizes { get; set; }
        [DataMember]
        public string Alimony { get; set; }
        [DataMember]
        public string Assets { get; set; }
        [DataMember]
        public string Fringe { get; set; }
        [DataMember]
        public string Other { get; set; }
        [DataMember]
        public string OtherDetails { get; set; }
    }

    [DataContract]
    public class RespIncome : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class IncomeRestService : Service
    {
        public IIncomeService IncomeService { get; set; }

        public object Post(ReqIncome request)
        {
            var income = request.TranslateTo<Income>();
            IncomeService.Add(income);
            return new RespIncome()
                {
                    Id = income.Id
                };
        }
        public object Put(ReqIncome request)
        {
            var income = request.TranslateTo<Income>();
            IncomeService.Update(income);
            return new RespIncome();
        }
    }
}