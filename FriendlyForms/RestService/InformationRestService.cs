using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Information/")]
    public class ReqInformation
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int InformationAccess { get; set; }
    }

    [DataContract]
    public class RespInformation : IHasResponseStatus
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class InformationRestService : Service
    {
        public IInformationService InformationService { get; set; }

        public object Post(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            InformationService.Add(information);
            return new RespInformation()
                {
                    Id = information.Id
                };
        }
        public object Put(ReqInformation request)
        {
            var information = request.TranslateTo<Information>();
            InformationService.Update(information);
            return new RespInformation();
        }
    }
}