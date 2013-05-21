using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
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
    public class ReqChild
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
    public class RespChild : IHasResponseStatus
    {
        [DataMember]
        public object Child { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
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
            return ChildService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Put(ReqChild request)
        {
            var child = request.TranslateTo<Child>();
            child.UserId = Convert.ToInt32(UserSession.CustomId);
            ChildService.Update(child);
            return null;
        }

        public object Delete(ReqChild request)
        {
            var child = request.TranslateTo<Child>();
            ChildService.Delete(child);
            return null;
        }
        public object Post(ReqChild request)
        {
            var childViewModel = request.TranslateTo<ChildViewModel>();
            childViewModel.UserId = Convert.ToInt32(UserSession.CustomId);
            var updatedChild = ChildService.AddOrUpdate(childViewModel);
            return new RespChild()
            {
                Child = new 
                    {
                        updatedChild.Id,
                        updatedChild.Name,
                        DateOfBirth = updatedChild.DateOfBirth == null ? "Not Provided" : updatedChild.DateOfBirth.Value.ToString("MM/dd/yyyy")
                    }
            };
        }
    }
}