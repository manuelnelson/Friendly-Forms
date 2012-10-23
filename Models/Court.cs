using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Court : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CountyId { get; set; }
        public string CaseNumber { get; set; }
        public int AuthorOfPlan { get; set; }
        public int PlanType { get; set; }
        public string ExistCaseNumber { get; set; }

        public IViewModel ConvertToModel()
        {
            return new CourtViewModel
            {
                AuthorOfPlan = AuthorOfPlan,
                CaseNumber = CaseNumber,
                CountyId = CountyId,
                PlanType = PlanType,
                ExistCaseNumber = ExistCaseNumber,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var update = (Court) entity;
            AuthorOfPlan = update.AuthorOfPlan;
            CaseNumber = update.CaseNumber;
            CountyId = update.CountyId;
            PlanType = update.PlanType;
            ExistCaseNumber = update.ExistCaseNumber;            
            UserId = update.UserId;
        }

   

    }
}
