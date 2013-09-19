using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;

namespace FriendlyForms.RestService
{
    public class ExtraExpenseFormRestService
    {
        //REST Resource DTO
        [Route("/ExtraExpenseForms")]
        [Route("/ExtraExpenseForms", "POST")]
        [Route("/ExtraExpenseForms/", "PUT")]
        [Route("/ExtraExpenseForms", "GET")]
        [DataContract]
        public class ExtraExpenseFormDto : IReturn<ExtraExpenseFormDto>, IHasUser
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public long[] Ids { get; set; }
            [DataMember]
            public long UserId { get; set; }
            [DataMember]
            public int HasExtraExpenses { get; set; }
        }
        [CanViewClientInfo]
        public class ExtraExpenseFormsService : ServiceBase
        {
            public IExtraExpenseFormService ExtraExpenseFormService { get; set; } //Injected by IOC

            public object Get(ExtraExpenseFormDto request)
            {
                if (request.Id != 0)
                {
                    return ExtraExpenseFormService.Get(request.Id);
                }
                return ExtraExpenseFormService.GetByUserId(request.UserId != 0 ? request.UserId : Convert.ToInt32(UserSession.CustomId));
            }

            public object Post(ExtraExpenseFormDto request)
            {
                var extraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Add(extraExpenseFormEntity);
                return extraExpenseFormEntity;
            }

            public object Put(ExtraExpenseFormDto request)
            {
                var extraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Update(extraExpenseFormEntity);
                return extraExpenseFormEntity;
            }

            public void Delete(ExtraExpenseFormDto request)
            {
                var extraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Delete(extraExpenseFormEntity);
            }
        }

    }

}
