using System;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using ServiceStack.Common;

namespace BusinessLogic
{
    public class OutputService : IOutputService
    {
        private IIncomeService IncomeService { get; set; }
        private IPreexistingSupportFormService PreexistingSupportFormService { get; set; }
        private IPreexistingSupportChildService PreexistingSupportChildService { get; set; }
        private IOtherChildrenService OtherChildrenService { get; set; }
        private IOtherChildService OtherChildService { get; set; }
        
        public OutputService(IIncomeService incomeService, IPreexistingSupportFormService preexistingSupportFormService, IOtherChildService otherChildService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService)
        {
            IncomeService = incomeService;
            PreexistingSupportChildService = preexistingSupportChildService;
            PreexistingSupportFormService = preexistingSupportFormService;
            OtherChildService = otherChildService;
            OtherChildrenService = otherChildrenService;
        }

        public ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false)
        {
            var income = IncomeService.GetByUserId(userId, isOtherParent).TranslateTo<IncomeDto>();
            var preexistingSupport = PreexistingSupportFormService.GetByUserId(userId, isOtherParent);
            var otherChildren = OtherChildrenService.GetByUserId(userId, isOtherParent);

            var schedule = new ScheduleB
            {
                GrossIncome = income.CalculateTotalIncome(),
                SelfEmploymentIncome = income.SelfIncome
            };
            schedule.FicaIncome = (int)(schedule.SelfEmploymentIncome * .62);
            schedule.MedicareTax = (int)(schedule.SelfEmploymentIncome * .0145);
            schedule.Total34 = schedule.FicaIncome + schedule.MedicareTax;
            schedule.Total5Minus1 = schedule.GrossIncome - schedule.Total34;
            if (preexistingSupport != null)
            {
                var preexistingSupportChildren = PreexistingSupportChildService.GetChildrenBySupportId(preexistingSupport.Id).ToList();
                schedule.PreexistingSupportChild = preexistingSupportChildren.ToList();
                schedule.PreexistingSupport = preexistingSupportChildren.Select(x => x.PreexistingSupport).ToList();
                schedule.TotalSupport = schedule.PreexistingSupport.Sum(c => c.Monthly);
            }
            schedule.AdjustedSupport = schedule.Total5Minus1 - schedule.TotalSupport;
            if (otherChildren != null)
            {
                schedule.OtherChildren = OtherChildService.GetChildrenByOtherChildrenId(otherChildren.Id).Select(x => x.TranslateTo<OtherChildDto>()).ToList();
                foreach (var otherChildDto in schedule.OtherChildren)
                {
                    otherChildDto.ClaimedBy = parentName;
                }
                schedule.OtherChildrenDescription = otherChildren.Details;
            }
            schedule.Subtotal = Math.Abs(schedule.Total5Minus1 - 0.0) > 0.01 ? schedule.Total5Minus1 : schedule.GrossIncome;
            //Todo: get this number
            schedule.GeorgiaObligations = 0;
            schedule.TheoreticalSupport = (int)(schedule.GeorgiaObligations * .75);
            schedule.PreexistingOrder = Math.Abs(schedule.AdjustedSupport - 0) > 0.01
                                            ? schedule.AdjustedSupport - schedule.TheoreticalSupport
                                            : schedule.Subtotal - schedule.TheoreticalSupport;
            return schedule;

        }
    }
}
