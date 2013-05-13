﻿using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ExtraExpenseRestService
    {
        //REST Resource DTO
        [Route("/ExtraExpense/{Ids}")]
        public class ExtraExpenseListDto : IReturn<List<ExtraExpenseDto>>
        {
            public long[] Ids { get; set; }

            public ExtraExpenseListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/ExtraExpense")]
        [Route("/ExtraExpense", "POST")]
        [Route("/ExtraExpense/", "PUT")]
        [Route("/ExtraExpense/{Id}", "GET")]
        public class ExtraExpenseDto : IReturn<ExtraExpenseDto>
        {
            public long Id { get; set; }
            public long UserId { get; set; }
            public long ChildId { get; set; }
            public int TutitionFather { get; set; }
            public int TutitionMother { get; set; }
            public int TutitionNonParent { get; set; }
            public int EducationFather { get; set; }
            public int EducationMother { get; set; }
            public int EducationNonParent { get; set; }
            public int MedicalFather { get; set; }
            public int MedicalMother { get; set; }
            public int MedicalNonParent { get; set; }
            public int SpecialFather { get; set; }
            public int SpecialMother { get; set; }
            public int SpecialNonParent { get; set; }
            public string SpecialDescriptionFather { get; set; }
            public string SpecialDescriptionMother { get; set; }
            public string SpecialDescriptionNonParent { get; set; }
            public int ExtraSpent { get; set; }
        }
        [Authenticate]
        public class ExtraExpensesService : ServiceBase
        {
            public IExtraExpenseService ExtraExpenseService { get; set; } //Injected by IOC

            public object Get(ExtraExpenseDto request)
            {
                if (request.ChildId != 0)
                    return ExtraExpenseService.GetByChildId(request.ChildId);
                return ExtraExpenseService.Get(request.Id);
            }

            public object Post(ExtraExpenseDto request)
            {
                var ExtraExpenseEntity = request.TranslateTo<ExtraExpense>();
                ExtraExpenseEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                ExtraExpenseService.Add(ExtraExpenseEntity);
                return ExtraExpenseEntity;
            }

            public object Put(ExtraExpenseDto request)
            {
                var ExtraExpenseEntity = request.TranslateTo<ExtraExpense>();
                ExtraExpenseEntity.UserId = Convert.ToInt32(UserSession.CustomId);
                ExtraExpenseService.Update(ExtraExpenseEntity);
                return ExtraExpenseEntity;
            }

            public void Delete(ExtraExpenseListDto request)
            {
                ExtraExpenseService.DeleteAll(request.Ids);
            }

            public void Delete(ExtraExpenseDto request)
            {
                var ExtraExpenseEntity = request.TranslateTo<ExtraExpense>();
                ExtraExpenseService.Delete(ExtraExpenseEntity);
            }
        }

    }

}
