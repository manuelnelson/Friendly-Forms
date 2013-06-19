using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;
using Property = Models.Property;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Property/")]
    public class ReqProperty
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int RealEstate { get; set; }
        [DataMember]
        public string RealEstateDescription { get; set; }
        [DataMember]
        public int PersonalProperty { get; set; }
        [DataMember]
        public string DividingProperty { get; set; }
    }

    [DataContract]
    public class RespProperty : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class PropertyRestService : ServiceBase
    {
        public IPropertyService PropertyService { get; set; }

        public object Post(ReqProperty request)
        {
            var property = request.TranslateTo<Property>();
            property.UserId = Convert.ToInt32(UserSession.CustomId);
            PropertyService.Add(property);
            return new RespProperty()
                {
                    Id = property.Id
                };
        }
        public object Put(ReqProperty request)
        {
            var property = request.TranslateTo<Property>();
            property.UserId = Convert.ToInt32(UserSession.CustomId);
            PropertyService.Update(property);
            return new RespProperty();
        }
    }

}