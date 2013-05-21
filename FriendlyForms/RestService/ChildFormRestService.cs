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
    [Route("/ChildForm/")]
    public class ReqChildForm
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
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

    public class ChildFormRestService : ServiceBase
    {
        public IChildFormService ChildFormService { get; set; }
        public object Get(ReqChildForm request)
        {
            if (request.Id != 0)
            {
                return ChildFormService.Get(request.Id);
            }
            return ChildFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
        }
        public object Post(ReqChildForm request)
        {
            var childFormViewModel = request.TranslateTo<ChildFormViewModel>();
            childFormViewModel.UserId = Convert.ToInt32(UserSession.CustomId);
            var updatedChildForm = ChildFormService.AddOrUpdate(childFormViewModel);
            return new RespChildForm()
                {
                    ChildForm = updatedChildForm
                };
        }
        public object Put(ReqChildForm request)
        {
            var childForm = request.TranslateTo<ChildForm>();
            childForm.UserId = Convert.ToInt32(UserSession.CustomId);
            ChildFormService.Update(childForm);
            return new RespChildForm();
        }
    }
}