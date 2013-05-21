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
    [Route("/House/")]
    public class ReqHouse
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int MaritalHouse { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string CityState { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
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
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class HouseRestService : ServiceBase
    {
        public IHouseService HouseService { get; set; }
        public object Get(ReqHouse request)
        {
            if (request.Id != 0)
            {
                return HouseService.Get(request.Id);
            }
            return HouseService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqHouse request)
        {
            var house = request.TranslateTo<House>();
            house.UserId = Convert.ToInt32(UserSession.CustomId);
            HouseService.Add(house);
            return new RespHouse()
                {
                    Id = house.Id
                };
        }
        public object Put(ReqHouse request)
        {
            var house = request.TranslateTo<House>();
            house.UserId = Convert.ToInt32(UserSession.CustomId);
            HouseService.Update(house);
            return new RespHouse();
        }
    }

}