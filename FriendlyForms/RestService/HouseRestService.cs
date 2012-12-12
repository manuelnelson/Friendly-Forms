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
    [Route("/House/")]
    public class ReqHouse
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int MaritalHouse { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int? RetailValue { get; set; }
        [DataMember]
        public int? MoneyOwed { get; set; }
        [DataMember]
        public int? Equity { get; set; }
        [DataMember]
        public string MortgageOwner { get; set; }
        [DataMember]
        public string Divide { get; set; }

    }

    [DataContract]
    public class RespHouse : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class HouseRestService : Service
    {
        public IHouseService HouseService { get; set; }

        public object Post(ReqHouse request)
        {
            var houseViewModel = request.TranslateTo<HouseViewModel>();
            HouseService.AddOrUpdate(houseViewModel);
            return new RespHouse();
        }
    }

}