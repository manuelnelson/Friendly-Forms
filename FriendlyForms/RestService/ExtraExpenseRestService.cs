using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ExtraExpenseRestService
    {
        [Route("/ExtraExpenses")]
        [Route("/ExtraExpenses", "POST")]
        [Route("/ExtraExpenses/", "PUT")]
        [Route("/ExtraExpenses/{Id}", "GET")]
        public class ExtraExpenseDto : IReturn<ExtraExpenseDto>, IHasUser
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
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
                ExtraExpenseService.Add(ExtraExpenseEntity);
                return ExtraExpenseEntity;
            }

            public object Put(ExtraExpenseDto request)
            {
                var ExtraExpenseEntity = request.TranslateTo<ExtraExpense>();
                ExtraExpenseService.Update(ExtraExpenseEntity);
                return ExtraExpenseEntity;
            }


            public void Delete(ExtraExpenseDto request)
            {
                var ExtraExpenseEntity = request.TranslateTo<ExtraExpense>();
                ExtraExpenseService.Delete(ExtraExpenseEntity);
            }
        }

    }

}
