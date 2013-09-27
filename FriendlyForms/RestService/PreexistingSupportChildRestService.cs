using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/PreexistingSupportChildren/")]
    [Route("/PreexistingSupportChildren/{Id}/")]
    public class ReqPreexistingSupportChild : IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public int Gender { get; set; }
        [DataMember]
        public int PreexistingSupportId { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupportChild : IHasResponseStatus
    {
        [DataMember]
        public PreexistingSupportChild Child { get; set; }
        [DataMember]
        public List<PreexistingSupportChild> Children { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class PreexistingSupportChildRestService : ServiceBase
    {
        public IPreexistingSupportChildService PreexistingSupportChildService { get; set; }
        public object Get(ReqPreexistingSupportChild request)
        {
            var childrenEntities = PreexistingSupportChildService.GetChildrenBySupportId(request.PreexistingSupportId).ToList();
            return new RespPreexistingSupportChild
                {
                    Children = childrenEntities
                };
        }
        public object Post(ReqPreexistingSupportChild request)
        {
            var preexistingSupportEntity = request.TranslateTo<PreexistingSupportChild>();
            PreexistingSupportChildService.Add(preexistingSupportEntity);
            return new RespPreexistingSupportChild()
                {
                    Child = preexistingSupportEntity
                };
        }
        public object Put(ReqPreexistingSupportChild request)
        {
            var preexistingSupportChild = request.TranslateTo<PreexistingSupportChild>();
            PreexistingSupportChildService.Update(preexistingSupportChild);
            return new RespPreexistingSupportChild();
        }
        public object Delete(ReqPreexistingSupportChild request)
        {
            if (request.Id > 0)
                PreexistingSupportChildService.Delete(request.Id);
            else if (request.PreexistingSupportId > 0)
                PreexistingSupportChildService.DeleteChildrenBySupportId(request.PreexistingSupportId);
            return null;
        }
    }
}