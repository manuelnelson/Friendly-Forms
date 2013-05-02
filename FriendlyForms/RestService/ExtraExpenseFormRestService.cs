﻿using System.Collections.Generic;
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
        [Route("/ExtraExpenseForm")]
        [Route("/ExtraExpenseForm/{Ids}")]
        public class ExtraExpenseFormListDto : IReturn<List<ExtraExpenseFormDto>>
        {
            public long[] Ids { get; set; }

            public ExtraExpenseFormListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/ExtraExpenseForm", "POST")]
        [Route("/ExtraExpenseForm/", "PUT")]
        [Route("/ExtraExpenseForm/{Id}", "GET")]
        public class ExtraExpenseFormDto : IReturn<ExtraExpenseFormDto>
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public int HasExtraExpenses { get; set; }
        }

        public class ExtraExpenseFormsService : Service
        {
            public IExtraExpenseFormService ExtraExpenseFormService { get; set; } //Injected by IOC

            public object Get(ExtraExpenseFormDto request)
            {
                return ExtraExpenseFormService.Get(request.Id);
            }

            public object Post(ExtraExpenseFormDto request)
            {
                var ExtraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Add(ExtraExpenseFormEntity);
                return ExtraExpenseFormEntity;
            }

            public object Put(ExtraExpenseFormDto request)
            {
                var ExtraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Update(ExtraExpenseFormEntity);
                return ExtraExpenseFormEntity;
            }

            public void Delete(ExtraExpenseFormListDto request)
            {
                ExtraExpenseFormService.DeleteAll(request.Ids);
            }

            public void Delete(ExtraExpenseFormDto request)
            {
                var ExtraExpenseFormEntity = request.TranslateTo<ExtraExpenseForm>();
                ExtraExpenseFormService.Delete(ExtraExpenseFormEntity);
            }
        }

    }

}
