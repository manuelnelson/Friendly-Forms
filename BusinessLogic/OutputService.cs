using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Helpers;
using BusinessLogic.Models;
using Models;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace BusinessLogic
{
    public class OutputService : IOutputService
    {
        private IIncomeService IncomeService { get; set; }
        private IPreexistingSupportFormService PreexistingSupportFormService { get; set; }
        private IPreexistingSupportChildService PreexistingSupportChildService { get; set; }
        private IPreexistingSupportService PreexistingSupportService { get; set; }
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
        private IChildFormService ChildFormService { get; set; }
        private IAddendumService AddendumService { get; set; }
        private IBcsoService BcsoService { get; set; }
        private IUserService UserService { get; set; }
        public OutputService(IIncomeService incomeService, IPreexistingSupportFormService preexistingSupportFormService, IOtherChildService otherChildService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService,
            ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionsService, IExtraDecisionsService extraDecisionsService,
            IHolidayService holidayService, IExtraHolidayService extraHolidayService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService,
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ITaxService taxService, ISpousalService spousalService,
            IChildSupportService childSupportService, IVehicleFormService vehicleFormService, IChildCareFormService childCareFormService, IExtraExpenseFormService extraExpenseFormService,
            IHealthService healthService, ISocialSecurityService socialSecurityService, IDeviationsService deviationsService, IChildFormService childFormService, IAddendumService addendumService, IPreexistingSupportService preexistingSupportService, IBcsoService bcsoService, IUserService userService)
        {
            UserService = userService;
            DeviationsService = deviationsService;
            ExtraExpenseFormService = extraExpenseFormService;
            HealthService = healthService;
            ChildCareFormService = childCareFormService;
            SocialSecurityService = socialSecurityService;
            BcsoService = bcsoService;
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
            ChildFormService = childFormService;
            AddendumService = addendumService;
            PreexistingSupportService = preexistingSupportService;
        }

        private const double Fica = .062;
        private const double MedicareTax = .0145;
        public ScheduleB GetScheduleB(long userId, string parentName, bool isOtherParent = false)
        {
            var income = IncomeService.GetByUserId(userId, isOtherParent).ToIncomeDto().ToMonthly();
            var preexistingSupport = PreexistingSupportFormService.GetByUserId(userId, isOtherParent);
            var otherChildren = OtherChildrenService.GetByUserId(userId, isOtherParent);
            var schedule = new ScheduleB
                {
                    GrossIncome = income.CalculateTotalIncome(),
                    SelfEmploymentIncome = income.SelfIncome,
                    OtherChildrenForm = otherChildren
                };
            schedule.FicaIncome = schedule.SelfEmploymentIncome * Fica;
            schedule.MedicareTax = schedule.SelfEmploymentIncome * MedicareTax;
            schedule.Total34 = schedule.FicaIncome + schedule.MedicareTax;
            schedule.Total5Minus1 = schedule.GrossIncome - schedule.Total34;
            if (preexistingSupport != null && preexistingSupport.Support == (int)YesNo.Yes)
            {
                var preexistingCourts = PreexistingSupportService.GetFiltered(x => x.UserId == userId && x.IsOtherParent == isOtherParent).ToList();
                foreach (var preexistingCourt in preexistingCourts)
                {
                    preexistingCourt.Children =
                        PreexistingSupportChildService.GetChildrenBySupportId(preexistingCourt.Id).ToList();
                    schedule.TotalSupport += preexistingCourt.Monthly;
                }
                schedule.PreexistingSupport = preexistingCourts;
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

                schedule.GeorgiaObligations = BcsoService.GetAmount(schedule.Total5Minus1, schedule.OtherChildren.Count);
                schedule.TheoreticalSupport = (schedule.GeorgiaObligations * .75);
                schedule.PreexistingOrder = Math.Abs(schedule.AdjustedSupport - 0) > 0.01
                                                ? schedule.AdjustedSupport - schedule.TheoreticalSupport
                                                : schedule.Subtotal - schedule.TheoreticalSupport;
            }
            schedule.Subtotal = Math.Abs(schedule.Total5Minus1 - 0) < 0.01 ? schedule.GrossIncome : schedule.Total5Minus1;
            schedule.IncomeDetails = income.OtherDetails;

            return schedule;
        }

        public List<IncompleteForm> GetParentingIncompleteForms(long userId)
        {
            var children = ChildService.GetByUserId(userId);
            var firstChild = children[0];
            var court = CourtService.GetByUserId(userId);
            var privacy = PrivacyService.GetByUserId(userId);
            var information = InformationService.GetByUserId(userId);
            var responsibility = ResponsibilityService.GetByUserId(userId);
            var communication = CommunicationService.GetByUserId(userId);
            var schedule = ScheduleService.GetByUserId(userId);
            var decisions = DecisionsService.GetChildrenListByUserId(userId);
            var holidays = HolidayService.GetChildrenListByUserId(userId);
            var addendum = AddendumService.GetByUserId(userId);
            var incompleteForms = new List<IncompleteForm>();
            if (court == null || !court.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Court",
                    Path = "/Domestic/House/User/" + userId,
                });
            }
            if (privacy == null || !privacy.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Privacy",
                    Path = "/Parenting/Supervision/User/" + userId,
                });
            }
            if (information == null || !information.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Information",
                    Path = "/Parenting/Information/User/" + userId,
                });
            }
            if (responsibility == null || !responsibility.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Responsibility",
                    Path = "/Parenting/Responsibility/User/" + userId,
                });
            }
            if (communication == null || !communication.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Communication",
                    Path = "/Parenting/Communication/User/" + userId,
                });
            }
            if (schedule == null || !schedule.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Schedule",
                    Path = "/Parenting/Schedule/User/" + userId,
                });
            }
            if (addendum == null || !addendum.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Special Considerations",
                    Path = "/Parenting/Addendum/User/" + userId,
                });
            }
            var names = IsFormCompleteForAllChildren(children, new List<IChildFormEntity>(decisions));
            if (names.Count > 0)
            {
                incompleteForms.Add(new IncompleteForm
                    {
                        Name = "Decisions - Children: " + names.Join(","),
                        Path = "/Parenting/Decision/User/" + userId + "/Child/" + firstChild.Id,
                    });
            }
            names = IsFormCompleteForAllChildren(children, new List<IChildFormEntity>(holidays));
            if (names.Count > 0)
            {
                incompleteForms.Add(new IncompleteForm
                    {
                        Name = "Holidays - Children:" + names.Join(","),
                        Path = "/Parenting/Holiday/User/" + userId + "/Child/" + firstChild.Id,
                    });
            } 
            return incompleteForms;
        }

        public List<IncompleteForm> GetDomesticIncompleteForms(long userId)
        {
            var house = HouseService.GetByUserId(userId);
            var property = PropertyService.GetByUserId(userId);
            var debt = DebtService.GetByUserId(userId);
            var assets = AssetService.GetByUserId(userId);
            var health = HealthInsuranceService.GetByUserId(userId);
            var taxes = TaxService.GetByUserId(userId);
            var vehicleForm = VehicleFormService.GetByUserId(userId);
            var spousal = SpousalService.GetByUserId(userId);
            var incompleteForms = new List<IncompleteForm>();
            if (house == null || !house.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Marital House",
                    Path = "/Domestic/House/User/" + userId,
                });
            }
            if (property == null || !property.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Property",
                    Path = "/Domestic/Property/User/" + userId,
                });
            }
            if (vehicleForm == null || !vehicleForm.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Vehicle",
                    Path = "/Domestic/Vehicle/User/" + userId,
                });
            }
            if (debt == null || !debt.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Debt",
                    Path = "/Domestic/Debt/User/" + userId,
                });
            }
            if (assets == null || !assets.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Assets",
                    Path = "/Domestic/Asset/User/" + userId,
                });
            }
            if (health == null || !health.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Health Insurance",
                    Path = "/Domestic/HealthInsurance/User/" + userId,
                });
            }
            if (spousal == null || !spousal.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Spousal Support",
                    Path = "/Domestic/Spousal/User/" + userId,
                });
            } 
            if (taxes == null || !taxes.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Taxes",
                    Path = "/Domestic/Tax/User/" + userId,
                });
            }

            return incompleteForms;
        }

        public List<IncompleteForm> GetFinancialIncompleteForms(long userId)
        {
            var children = ChildService.GetByUserId(userId);
            var firstChild = children[0];
            var childCare = ChildCareFormService.GetByUserId(userId);
            var childSupport = ChildSupportService.GetByUserId(userId);
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
            var incompleteForms = new List<IncompleteForm>();
            if (childCare == null || !childCare.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Child Care",
                    Path = "/Financial/ChildCare/User/" + userId + "/Child/" + firstChild.Id,
                });
            }
            if (childSupport == null || !childSupport.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Child Support",
                    Path = "/Financial/ChildSupport/User/" + userId,
                });
            } 
            if (extraExpense == null || !extraExpense.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Extra Expenses",
                    Path = "/Financial/ExtraExpense/User/" + userId + "/Child/" + firstChild.Id,
                });
            }
            if (health == null || !health.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Health Insurance",
                    Path = "/Financial/Health/User/" + userId,
                });
            }
            if (incomeFather == null || !incomeFather.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Income (Father)",
                    Path = "/Financial/Income/User/" + userId + "/false",
                });
            }
            if (incomeMother == null || !incomeMother.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Income (Mother)",
                    Path = "/Financial/Income/User/" + userId + "/true",
                });
            }
            if (socialSecurityFather == null || !socialSecurityFather.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Social Security (Father)",
                    Path = "/Financial/SocialSecurity/User/" + userId + "/false",
                });
            }
            if (socialSecurityMother == null || !socialSecurityMother.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Social Security (Mother)",
                    Path = "/Financial/SocialSecurity/User/" + userId + "/true",
                });
            }
            if (preexistingSupportFather == null || !preexistingSupportFather.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Preexisting Support (Father)",
                    Path = "/Financial/Support/User/" + userId + "/false",
                });
            }
            if (preexistingSupportMother == null || !preexistingSupportMother.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Preexisting Support (Mother)",
                    Path = "/Financial/Support/User/" + userId + "/true",
                });
            }
            if (otherChildrenFormFather == null || !otherChildrenFormFather.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Other Children (Father)",
                    Path = "/Financial/OtherChild/User/" + userId + "/false",
                });
            }
            if (otherChildrenFormMother == null || !otherChildrenFormMother.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Other Children (Mother)",
                    Path = "/Financial/OtherChild/User/" + userId + "/true",
                });
            }
            if (deviations == null || !deviations.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Deviations",
                    Path = "/Financial/Deviation/User/" + userId,
                });
            }
            return incompleteForms;
        }

        public List<IncompleteForm> GetStarterIncompleteForms(long userId)
        {
            var court = CourtService.GetByUserId(userId);
            var participants = ParticipantService.GetByUserId(userId);
            var childForm = ChildFormService.GetByUserId(userId);
            var user = UserService.Get(userId);
            var incompleteForms = new List<IncompleteForm>();
            if (user == null || !user.Verified)
            {
                incompleteForms.Add(new IncompleteForm
                {
                    Name = "Beta Agreement",
                    Path = "/Starter/BetaAgreement/User/" + userId
                });                                
            }
            if (court == null || !court.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                    {
                        Name = "Court",
                        Path = "/Starter/Court/User/" + userId
                    });                
            }
            if (participants == null || !participants.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                    {
                        Name = "Participants",
                        Path = "/Starter/Participant/User/" + userId
                    });                
            }
            if (childForm == null || !childForm.IsValid())
            {
                incompleteForms.Add(new IncompleteForm
                    {
                        Name = "Children",
                        Path = "/Starter/Children/User/" + userId
                    });                
            }
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
                if (childFormEntity == null || !childFormEntity.IsValid())
                    names.Add(child.Name);
            }
            return names;
        }
    }
}
