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
    [Route("/ChildForm/")]
    public class ReqChildForm
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int ChildrenInvolved { get; set; }
    }

    [DataContract]
    public class RespChildForm : IHasResponseStatus
    {
        [DataMember]
        public ChildForm ChildForm { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ChildFormRestService : Service
    {
        public IChildFormService ChildFormService { get; set; }

        public object Post(ReqChildForm request)
        {
            var childFormViewModel = request.TranslateTo<ChildFormViewModel>();
            var updatedChildForm = ChildFormService.AddOrUpdate(childFormViewModel);
            return new RespChildForm()
                {
                    ChildForm = updatedChildForm
                };
        }
        public object Put(ReqChildForm request)
        {
            var childForm = request.TranslateTo<ChildForm>();
            ChildFormService.Update(childForm);
            return new RespChildForm();
        }
    }
}