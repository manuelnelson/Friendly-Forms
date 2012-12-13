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
    [Route("/Child/")]
    public class ReqChild
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
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

    public class ChildRestService : Service
    {
        public IChildService ChildService { get; set; }

        public object Post(ReqChild request)
        {
            var childViewModel = request.TranslateTo<ChildViewModel>();
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