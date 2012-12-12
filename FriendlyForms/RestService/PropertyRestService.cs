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
    [Route("/Property/")]
    public class ReqProperty
    {
        [DataMember]
        public int UserId { get; set; }
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
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class PropertyRestService : Service
    {
        public IPropertyService PropertyService { get; set; }

        public object Post(ReqProperty request)
        {
            var propertyViewModel = request.TranslateTo<PropertyViewModel>();
            PropertyService.AddOrUpdate(propertyViewModel);
            return new RespProperty();
        }
    }

}