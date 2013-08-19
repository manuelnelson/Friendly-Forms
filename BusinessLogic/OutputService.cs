using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using Models;
using Models.Contract;
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
        private ICourtService CourtService { get; set; }
        private IParticipantService ParticipantService { get; set; }
        private IChildService ChildService { get; set; }
        private IPrivacyService PrivacyService { get; set; }
        private IInformationService InformationService { get; set; }
        private IDecisionsService DecisionsService { get; set; }
        private IHolidayService HolidayService { get; set; }
        private IExtraDecisionsService ExtraDecisionsService { get; set; }
        private IExtraHolidayService ExtraHolidayService { get; set; }
        private IResponsibilityService ResponsibilityService { get; set; }
        private ICommunicationService CommunicationService { get; set; }
        private IScheduleService ScheduleService { get; set; }
        private IHouseService HouseService { get; set; }
        private IPropertyService PropertyService { get; set; }
        private IVehicleService VehicleService { get; set; }
        private IDebtService DebtService { get; set; }
        private IAssetService AssetService { get; set; }
        private IHealthInsuranceService HealthInsuranceService { get; set; }
        private ISpousalService SpousalService { get; set; }
        private IChildSupportService ChildSupportService { get; set; }
        private IVehicleFormService VehicleFormService { get; set; }
        private ITaxService TaxService { get; set; }
        private IChildCareFormService ChildCareFormService { get; set; }
        private IExtraExpenseFormService ExtraExpenseFormService { get; set; }
        private IHealthService HealthService { get; set; }
        private ISocialSecurityService SocialSecurityService { get; set; }
        private IDeviationsService DeviationsService { get; set; }

        public OutputService(IIncomeService incomeService, IPreexistingSupportFormService preexistingSupportFormService, IOtherChildService otherChildService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService,
            ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionsService, IExtraDecisionsService extraDecisionsService,
            IHolidayService holidayService, IExtraHolidayService extraHolidayService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService, 
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ITaxService taxService, ISpousalService spousalService,
            IChildSupportService childSupportService, IVehicleFormService vehicleFormService, IChildCareFormService childCareFormService, IExtraExpenseFormService extraExpenseFormService,
            IHealthService healthService, ISocialSecurityService socialSecurityService, IDeviationsService deviationsService)
        {
            DeviationsService = deviationsService;
            ExtraExpenseFormService = extraExpenseFormService;
            HealthService = healthService;
            ChildCareFormService = childCareFormService;
            SocialSecurityService = socialSecurityService;
            IncomeService = incomeService;
            PreexistingSupportChildService = preexistingSupportChildService;
            PreexistingSupportFormService = preexistingSupportFormService;
            OtherChildService = otherChildService;
            OtherChildrenService = otherChildrenService;
            CourtService = courtService;
            ParticipantService = participantService;
            ChildService = childService;
            PrivacyService = privacyService;
            InformationService = informationService;
            DecisionsService = decisionsService;
            HolidayService = holidayService;
            ExtraDecisionsService = extraDecisionsService;
            ExtraHolidayService = extraHolidayService;
            ResponsibilityService = responsibilityService;
            CommunicationService = communicationService;
            ScheduleService = scheduleService;
            HouseService = houseService;
            PropertyService = propertyService;
            VehicleService = vehicleService;
            DebtService = debtService;
            AssetService = assetService;
            HealthInsuranceService = healthInsuranceService;
            TaxService = taxService;
            SpousalService = spousalService;
            ChildSupportService = childSupportService;
            VehicleFormService = vehicleFormService;
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

        public List<string> GetParentingIncompleteForms(long userId)
        {
            var children = ChildService.GetByUserId(userId);

            var court = CourtService.GetByUserId(userId);
            var privacy = PrivacyService.GetByUserId(userId);
            var information = InformationService.GetByUserId(userId);
            var responsibility = ResponsibilityService.GetByUserId(userId);
            var communication = CommunicationService.GetByUserId(userId);
            var schedule = ScheduleService.GetByUserId(userId);

            var decisions = DecisionsService.GetChildrenListByUserId(userId);
            var holidays = HolidayService.GetChildrenListByUserId(userId);
            
            var incompleteForms = new List<string>();
            if(court != null && !court.IsValid())
                incompleteForms.Add("Court");
            if (privacy != null && !privacy.IsValid())
                incompleteForms.Add("Privacy");
            if (information != null && !information.IsValid())
                incompleteForms.Add("Information");
            if (responsibility != null && !responsibility.IsValid())
                incompleteForms.Add("Responsibility");
            if (communication != null && !communication.IsValid())
                incompleteForms.Add("Communication");
            if (schedule != null && !schedule.IsValid())
                incompleteForms.Add("Schedule");
            var names = IsFormCompleteForAllChildren(children, new List<IChildFormEntity>(decisions));
            if (names.Count > 0)
            {
                incompleteForms.Add("Decisions - children: " + names.Join(","));
            }
            names = IsFormCompleteForAllChildren(children, new List<IChildFormEntity>(holidays));
            if (names.Count > 0)
            {
                incompleteForms.Add("Holidays - children:" + names.Join(","));
            } 
            return incompleteForms;
        }

        public List<string> GetDomesticIncompleteForms(long userId)
        {
            var house = HouseService.GetByUserId(userId);
            var property = PropertyService.GetByUserId(userId);
            var debt = DebtService.GetByUserId(userId);
            var assets = AssetService.GetByUserId(userId);
            var health = HealthInsuranceService.GetByUserId(userId);
            var taxes = TaxService.GetByUserId(userId);
            var support = SpousalService.GetByUserId(userId);
            var vehicleForm = VehicleFormService.GetByUserId(userId);

            var incompleteForms = new List<string>();
            if (!house.IsValid())
                incompleteForms.Add("Marital House");
            if (!property.IsValid())
                incompleteForms.Add("Property");
            if (!debt.IsValid())
                incompleteForms.Add("Debt");
            if (!assets.IsValid())
                incompleteForms.Add("Assets");
            if (!health.IsValid())
                incompleteForms.Add("Health Insurance");
            if (!taxes.IsValid())
                incompleteForms.Add("Taxes");
            if (!support.IsValid())
                incompleteForms.Add("Child Support");
            if (!vehicleForm.IsValid())
                incompleteForms.Add("Vehicle");
            if (!taxes.IsValid())
                incompleteForms.Add("Taxes");

            return incompleteForms;            
        }

        public List<string> GetFinancialIncompleteForms(long userId)
        {
            var children = ChildService.GetByUserId(userId);

            var childCare = ChildCareFormService.GetByUserId(userId);
            var extraExpense = ExtraExpenseFormService.GetByUserId(userId);
            var health = HealthService.GetByUserId(userId);
            var incomeFather = IncomeService.GetByUserId(userId);
            var incomeMother = IncomeService.GetByUserId(userId, isOtherParent: true);
            var socialSecurityFather = SocialSecurityService.GetByUserId(userId);
            var socialSecurityMother = SocialSecurityService.GetByUserId(userId, isOtherParent: true);
            var preexistingSupportFather = PreexistingSupportFormService.GetByUserId(userId);
            var preexistingSupportMother = PreexistingSupportFormService.GetByUserId(userId, isOtherParent: true);
            var otherChildrenFormFather = OtherChildrenService.GetByUserId(userId);
            var otherChildrenFormMother = OtherChildrenService.GetByUserId(userId, isOtherParent: true);
            var deviations = DeviationsService.GetByUserId(userId);

            //Validate
            var incompleteForms = new List<string>();
            if (childCare == null || !childCare.IsValid())
                incompleteForms.Add("Child Care");
            if (extraExpense == null || !extraExpense.IsValid())
                incompleteForms.Add("Extra Expenses");
            if (health == null || !health.IsValid())
                incompleteForms.Add("Health Insurance");
            if (health == null || !incomeFather.IsValid())
                incompleteForms.Add("Income (Father)");
            if (incomeMother == null || !incomeMother.IsValid())
                incompleteForms.Add("Income (Mother)");
            if (socialSecurityFather == null || !socialSecurityFather.IsValid())
                incompleteForms.Add("Social Security (Father)");
            if (socialSecurityMother == null || !socialSecurityMother.IsValid())
                incompleteForms.Add("Social Security (Mother)");
            if (preexistingSupportFather == null || !preexistingSupportFather.IsValid())
                incompleteForms.Add("Preexisting Support (Father)");
            if (preexistingSupportMother == null || !preexistingSupportMother.IsValid())
                incompleteForms.Add("Preexisting Support (Mother)");
            if (otherChildrenFormFather == null || !otherChildrenFormFather.IsValid())
                incompleteForms.Add("Other Children (Father)");
            if (otherChildrenFormMother == null || !otherChildrenFormMother.IsValid())
                incompleteForms.Add("Other Children (Mother)");
            if (deviations == null || !deviations.IsValid())
                incompleteForms.Add("Deviations");

            return incompleteForms;

        }

        /// <summary>
        /// Go's through all children forms.  If form is incomplete, adds the child's name to a list that is returned.
        /// </summary>
        /// <param name="children"></param>
        /// <param name="childFormEntities"></param>
        /// <returns>List of All Children with Incomplete Form</returns>
        private List<string> IsFormCompleteForAllChildren(IEnumerable<Child> children, List<IChildFormEntity> childFormEntities)
        {
            var names = new List<string>();
            foreach (var child in children)
            {
                var childFormEntity = childFormEntities.FirstOrDefault(x => x.ChildId == child.Id);
                if(childFormEntity != null && !childFormEntity.IsValid())
                    names.Add(child.Name);
            }
            return names;
        }
    }
}
