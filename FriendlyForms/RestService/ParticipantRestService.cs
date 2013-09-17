using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    #region Request Objects
    [DataContract]
    [Route("/Participants/")]
    public class ReqParticipant : IReturn<RespParticipant>
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string PlaintiffsName { get; set; }
        [DataMember]
        public int PlaintiffRelationship { get; set; }
        [DataMember]
        public int PlaintiffCustodialParent { get; set; }
        [DataMember]
        public string DefendantsName { get; set; }
        [DataMember]
        public int DefendantRelationship { get; set; }
        [DataMember]
        public int DefendantCustodialParent { get; set; }
        [DataMember]
        public int IsJointCustody { get; set; }
    }
    
    [DataContract]
    [Route("/Participants/CustodyInfomation")]
    public class ReqCustody
    {
        [DataMember]
        public long UserId { get; set; }
    }
    #endregion 
    #region Response objects
    [DataContract]
    public class RespParticipant 
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    [Route("/Participants/CustodyInfomation")]
    public class RespCustody
    {
        [DataMember]
        public CustodyInformation CustodyInformation { get; set; }
    }    

    #endregion
    [Authenticate]
    public class ParticipantRestService : ServiceBase
    {
        public IParticipantService ParticipantService { get; set; }
        public object Get(ReqParticipant request)
        {
            if (request.Id != 0)
            {
                return ParticipantService.Get(request.Id);
            }
            return ParticipantService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }

        public object Get(ReqCustody request)
        {
            return new RespCustody
            {
                CustodyInformation = ParticipantService.GetCustodyInformation(request.UserId)
            };
        }

        public object Post(ReqParticipant request)
        {
            var participant = request.TranslateTo<Participant>();
            ParticipantService.Add(participant);
            return new RespParticipant()
                {
                    Id = participant.Id
                };
        }
        public object Put(ReqParticipant request)
        {
            var participant = request.TranslateTo<Participant>();
            ParticipantService.Update(participant);
            return new RespParticipant();
        }
    }
}