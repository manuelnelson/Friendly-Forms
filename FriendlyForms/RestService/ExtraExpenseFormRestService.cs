﻿using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ExtraExpenseFormRestService
    {
        //REST Resource DTO
        [Route("/ExtraExpenseForms")]
        [Route("/ExtraExpenseForms/{Ids}")]
        public class ExtraExpenseFormListDto : IReturn<List<ExtraExpenseFormDto>>
        {
            public long[] Ids { get; set; }

            public ExtraExpenseFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/ExtraExpenseForms", "POST")]
        [Route("/ExtraExpenseForms/", "PUT")]
        [Route("/ExtraExpenseForms", "GET")]
        public class ExtraExpenseFormDto : IReturn<ExtraExpenseFormDto>
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public int HasExtraExpenses { get; set; }
        }
        [Authenticate]
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

            public void Delete(ExtraExpenseFormListDto request)
            {
                ExtraExpenseFormService.DeleteAll(request.Ids);
            }

            public void Delete(ExtraExpenseFormDto request)
            {
                var extraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Delete(extraExpenseFormEntity);
            }
        }

    }

}
