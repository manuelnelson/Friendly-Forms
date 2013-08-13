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
    [Route("/Supports/")]
    public class ReqPreexistingSupport
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public bool IsOtherParent { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int Support { get; set; }
        [DataMember]
        public string CourtName { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string OrderDate { get; set; }
        [DataMember]
        public string Monthly { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupport : IHasResponseStatus
    {
        [DataMember]
        public ReqPreexistingSupport PreexistingSupport { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class PreexistingSupportRestService : ServiceBase
    {
        public IPreexistingSupportService PreexistingSupportService { get; set; }
        public object Get(ReqPreexistingSupport request)
        {
            if (request.Id != 0)
            {
                return PreexistingSupportService.Get(request.Id);
            }
            return PreexistingSupportService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqPreexistingSupport request)
        {
            var preexistingSupportEntity = request.TranslateTo<PreexistingSupport>();
            preexistingSupportEntity.UserId = Convert.ToInt32(UserSession.CustomId);
            PreexistingSupportService.Add(preexistingSupportEntity);
            var preexistSupport = preexistingSupportEntity.TranslateTo<ReqPreexistingSupport>();
            preexistSupport.OrderDate = preexistingSupportEntity.OrderDate.ToShortDateString();
            return new RespPreexistingSupport()
                {
                    PreexistingSupport = preexistSupport
                };
        }
        public object Put(ReqPreexistingSupport request)
        {
            var preexistingSupport = request.TranslateTo<PreexistingSupport>();
            preexistingSupport.UserId = Convert.ToInt32(UserSession.CustomId);
            PreexistingSupportService.Update(preexistingSupport);
            return new RespPreexistingSupport();
        }
    }
}