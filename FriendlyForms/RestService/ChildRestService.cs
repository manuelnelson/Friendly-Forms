using System;
using System.Collections.Generic;
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
    [Route("/Child/")]
    [Route("/Child/", "PUT")]
    [Route("/Child/", "DELETE")]
    public class ReqChild : IReturn<RespChild>
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
        public int ChildFormId { get; set; }
    }

    [DataContract]
    public class RespChild
    {
        [DataMember]
        public object Child { get; set; }
        [DataMember]
        public List<Child> Children { get; set; }
    }
    [Authenticate]
    public class ChildRestService : ServiceBase
    {
        public IChildService ChildService { get; set; }
        public object Get(ReqChild request)
        {
            if (request.Id != 0)
            {
                return ChildService.Get(request.Id);
            }
            return new RespChild()
                {
                    Children = ChildService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId))
                };
        }
        public object Put(ReqChild request)
        {
            var child = request.TranslateTo<Child>();
            ChildService.Update(child);
            return null;
        }

        public object Delete(ReqChild request)
        {
            ChildService.Delete(request.Id);
            return null;
        }
        public object Post(ReqChild request)
        {
            var child = request.TranslateTo<Child>();
            ChildService.Add(child);
            return new RespChild()
            {
                Child = new 
                    {
                        child.Id,
                        child.Name,
                        DateOfBirth = child.DateOfBirth == null ? "Not Provided" : child.DateOfBirth.Value.ToString("MM/dd/yyyy"),
                        child.DateOfBirthString
                    }
            };
        }
    }
}