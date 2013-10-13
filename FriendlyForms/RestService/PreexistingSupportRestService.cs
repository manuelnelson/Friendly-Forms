using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Supports/")]
    public class ReqPreexistingSupport : IReturn<RespPreexistingSupport>, IHasUser
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
        public string StateId { get; set; }
        [DataMember]
        public string CaseNumber { get; set; }
        [DataMember]
        public string OrderDate { get; set; }
        [DataMember]
        public int Monthly { get; set; }
    }

    [DataContract]
    public class RespPreexistingSupport
    {
        [DataMember]
        public List<PreexistingSupport> PreexistingSupports { get; set; }
    }
    [CanViewClientInfo]
    public class PreexistingSupportRestService : ServiceBase
    {
        public IPreexistingSupportService PreexistingSupportService { get; set; }
        public object Get(ReqPreexistingSupport request)
        {
            if (request.Id != 0)
            {
                return PreexistingSupportService.Get(request.Id);
            }
            return new RespPreexistingSupport
                {
                    PreexistingSupports =
                        PreexistingSupportService.GetByUserId(
                            request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId),
                            request.IsOtherParent)
                };
        }
        public object Post(ReqPreexistingSupport request)
        {
            var preexistingSupportEntity = request.TranslateTo<PreexistingSupport>();
            PreexistingSupportService.Add(preexistingSupportEntity);
            return preexistingSupportEntity;
        }
        public object Put(ReqPreexistingSupport request)
        {
            var preexistingSupport = request.TranslateTo<PreexistingSupport>();
            PreexistingSupportService.Update(preexistingSupport);
            return preexistingSupport;
        }
        public object Delete(ReqPreexistingSupport request)
        {
            PreexistingSupportService.Delete(request.Id);
            return null;

        }
    }
}