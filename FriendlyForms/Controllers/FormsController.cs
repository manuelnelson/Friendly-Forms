using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.Controllers
{
    public class FormsController : ControllerBase
    {
        private readonly ICourtService _courtService;
        private readonly IParticipantService _participantService;
        private readonly IChildService _childService;
        private readonly IChildFormService _childFormService;
        private readonly IPrivacyService _privacyService;
        private readonly IInformationService _informationService;
        private readonly IDecisionsService _decisionsService;
        private readonly IResponsibilityService _responsibilityService;
        private readonly ICommunicationService _communicationService;
        private readonly IScheduleService _scheduleService;
        private readonly ICountyService _countyService;
        private readonly IHouseService _houseService;
        private readonly IPropertyService _propertyService;
        private readonly IVehicleService _vehicleService;
        private readonly IDebtService _debtService;
        private readonly IAssetService _assetService;
        private readonly IHealthInsuranceService _healthInsuranceService;
        private readonly ISpousalService _spousalService;
        private readonly ITaxService _taxService;
        private readonly IChildSupportService _childSupportService;
        private readonly IHolidayService _holidayService;
        private readonly IIncomeService _incomeService;
        private readonly ISocialSecurityService _socialSecurityService;
        private readonly IPreexistingSupportService _preexistingSupportService;
        private readonly IOtherChildrenService _otherChildrenService;
        private readonly IOtherChildService _otherChildService;
        private readonly IVehicleFormService _vehicleFormService;
        private readonly IAddendumService _addendumService;
        private readonly IHealthService _healthService;
        private readonly IChildCareFormService _childCareFormService;
        //
        // GET: /Forms/
        public FormsController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService,ICountyService countyService,
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IIncomeService incomeService, ISocialSecurityService socialSecurityService, 
            IPreexistingSupportService preexistingSupportService, IOtherChildrenService otherChildrenService, IOtherChildService otherChildService, IVehicleFormService vehicleFormService, IChildFormService childFormService, IAddendumService addendumService, IHealthService healthService, IChildCareFormService childCareFormService)
        {
            _courtService = courtService;
            _participantService = participantService;
            _childService = childService;
            _privacyService = privacyService;
            _informationService = informationService;
            _decisionsService = decisionService;
            _responsibilityService = responsibilityService;
            _communicationService = communicationService;
            _scheduleService = scheduleService;
            _countyService = countyService;
            _houseService = houseService;
            _propertyService = propertyService;
            _vehicleService = vehicleService;
            _debtService = debtService;
            _assetService = assetService;
            _healthInsuranceService = healthInsuranceService;
            _spousalService = spousalService;
            _taxService = taxService;
            _childSupportService = childSupportService;
            _holidayService = holidayService;
            _incomeService = incomeService;
            _socialSecurityService = socialSecurityService;
            _preexistingSupportService = preexistingSupportService;
            _otherChildrenService = otherChildrenService;
            _otherChildService = otherChildService;
            _vehicleFormService = vehicleFormService;
            _childFormService = childFormService;
            _addendumService = addendumService;
            _healthService = healthService;
            _childCareFormService = childCareFormService;
        }
        
        [Authenticate]
        public ActionResult Starter()
        {
            var Id = Convert.ToInt32(UserSession.CustomId);
            var court = _courtService.GetByUserId(Id) as CourtViewModel;
            var participants = _participantService.GetByUserId(Id);
            var children = _childService.GetByUserId(Id);
            var childForm = _childFormService.GetByUserId(Id);
            var counties = _countyService.GetAll();
            court.Counties = counties;

            var formsViewModel = new StarterFormsCompleted()
            {
                Children = childForm.UserId != 0,
                Participant = participants.UserId != 0,
            };

            var starterViewModel = new StarterViewModel
                {
                    CourtViewModel = court,
                    ParticipantViewModel = participants as ParticipantViewModel,
                    ChildAllViewModel = new ChildAllViewModel()
                        {
                            ChildViewModel = children,
                            ChildFormViewModel = childForm as ChildFormViewModel
                        },
                    StarterFormsCompleted = formsViewModel,
                };
            return View(starterViewModel);
        }
        [Authenticate]
        public ActionResult Parenting()
        {
            var Id = Convert.ToInt32(UserSession.CustomId);
            var court = _courtService.GetByUserId(Id) as CourtViewModel;
            var participants = _participantService.GetByUserId(Id) as ParticipantViewModel;
            var children = _childService.GetByUserId(Id);
            var childForm = _childFormService.GetByUserId(Id);
            var privacy = _privacyService.GetByUserId(Id);
            var decisions = _decisionsService.GetByUserId(Id);
            var information = _informationService.GetByUserId(Id);
            var responsibility = _responsibilityService.GetByUserId(Id);
            var communication = _communicationService.GetByUserId(Id);
            var schedule = _scheduleService.GetByUserId(Id) as ScheduleViewModel;
            var custodyInformation = _participantService.GetCustodyInformation(participants);
            if (schedule != null)
            {
                schedule.NonCustodialParent = custodyInformation.NonCustodyParent;
                //schedule.CustodialParent = custodyInformation.CustodyParent;
            }

            var holiday = children.Children.Any() ? _holidayService.GetByChildId(children.Children.First().Id) : new Holiday();
            var addendum = _addendumService.GetByUserId(Id);
            var allDecisions = new AllDecisionsViewModel()
                {
                    DecisionsViewModel = new DecisionsViewModel()
                };
            var allHolidays = new AllHolidaysViewModel
                {
                    HolidayViewModel = new HolidayViewModel()
                };
            var counties = _countyService.GetAll();
            court.Counties = counties;

            var formsViewModel = new FormsCompleted()
                {
                    Children = childForm.UserId != 0,
                    Communication = communication.UserId != 0,
                    Decisions = decisions.UserId != 0,
                    Holiday = holiday != null,
                    Information = information.UserId != 0,
                    Participant = participants.UserId != 0,
                    Privacy = privacy.UserId != 0,
                    Responsibility = responsibility.UserId != 0,
                    Schedule = schedule.UserId != 0,
                    Addendum = addendum.UserId != 0
                };

            var childViewModel = new ParentingPlanViewModel
                {
                    CourtViewModel = court,
                    ParticipantViewModel = participants,
                    ChildAllViewModel = new ChildAllViewModel()
                        {
                            ChildViewModel = children,
                            ChildFormViewModel = childForm as ChildFormViewModel
                        },
                    PrivacyViewModel = privacy as PrivacyViewModel,
                    InformationViewModel = information as InformationViewModel,
                    AllDecisionsViewModel = allDecisions,
                    ResponsibilityViewModel = responsibility as ResponsibilityViewModel,
                    CommunicationViewModel = communication as CommunicationViewModel,
                    ScheduleViewModel = schedule,
                    HolidayViewModel = allHolidays,
                    AddendumViewModel = addendum as AddendumViewModel,
                    FormsCompleted = formsViewModel
                };
            return View(childViewModel);
        }

        [Authenticate]
        public ActionResult DomesticMediation()
        {
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var Id = Convert.ToInt32(UserSession.CustomId);
            var house = _houseService.GetByUserId(Id);
            var property = _propertyService.GetByUserId(Id);            
            var debt = _debtService.GetByUserId(Id);
            var assets = _assetService.GetByUserId(Id);
            var health= _healthInsuranceService.GetByUserId(Id);
            var spousal = _spousalService.GetByUserId(Id);
            var taxes = _taxService.GetByUserId(Id);
            var participants = _participantService.GetByUserId(Id) as ParticipantViewModel;
            List<SelectListItem> nameList;
            if (participants != null)
            {
                nameList = new List<SelectListItem>()
                    {
                        new SelectListItem() {Text = participants.PlaintiffsName, Value = participants.PlaintiffsName},
                        new SelectListItem() {Text = participants.DefendantsName, Value = participants.DefendantsName}
                    };
            } else
            {
                nameList = new List<SelectListItem>();
            }
            var support = _childSupportService.GetByUserId(Id);
            var vehicleForm = _vehicleFormService.GetByUserId(Id);
            var vehicles = _vehicleService.GetByUserId(Id).ToList();
            var vehicleModel = new VehicleViewModel()
                {
                    VehicleList = vehicles,
                    Names = nameList
                };
            var formsViewModel = new FormsCompletedDomestic()
            {
                AssetCompleted = assets.UserId != 0,
                RealEstateCompleted = property.UserId !=0,
                DebtCompleted = debt.UserId != 0,
                HealthCompleted = health.UserId != 0,
                SpousalCompleted = spousal.UserId != 0,
                TaxCompleted = taxes.UserId != 0,
                ChildCompleted = support.UserId != 0,
                VehicleCompleted = vehicleForm.UserId != 0
            };
            var domesticModel = new DomesticMediationViewModel
            {
                HouseViewModel = house as HouseViewModel,
                PropertyViewModel = property as PropertyViewModel,
                VehicleAllViewModel = new VehicleAllViewModel()
                    {
                        VehicleViewModel = vehicleModel,
                        VehicleFormViewModel = vehicleForm as VehicleFormViewModel
                    },
                DebtViewModel = debt as DebtViewModel,
                AssetViewModel = assets as AssetViewModel,
                HealthInsuranceViewModel = health as HealthInsuranceViewModel,
                SpousalViewModel = spousal as SpousalViewModel,
                TaxViewModel = taxes as TaxViewModel,
                ChildSupportViewModel = support as ChildSupportViewModel,
                FormsCompleted = formsViewModel,
                //TODO: we'll need to do a check to see if there are children involved.  This will be changed later but for NOW
                HasChildren = true
            };
            return View(domesticModel);
        }

        [Authenticate]
        public ActionResult Financial()
        {
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var Id = Convert.ToInt32(UserSession.CustomId);
            var income = _incomeService.GetByUserId(Id);
            var incomeOther = _incomeService.GetByUserId(Id, isOtherParent:true);
            var children = _childService.GetByUserId(Id);
            var childForm = _childFormService.GetByUserId(Id);
            var social = _socialSecurityService.GetByUserId(Id);
            var socialOther = _socialSecurityService.GetByUserId(Id, isOtherParent:true);
            var preexistList = _preexistingSupportService.GetByUserId(Id);
            var preexistListOther = _preexistingSupportService.GetByUserId(Id, isOtherParent:true);
            var other = _otherChildrenService.GetByUserId(Id);
            var otherChildren = _otherChildService.GetChildrenByOtherChildrenId(other.Id);
            var otherOther = _otherChildrenService.GetByUserId(Id, isOtherParent: true);
            var otherChildrenOther = _otherChildService.GetChildrenByOtherChildrenId(otherOther.Id);
            var health = _healthService.GetByUserId(Id) as HealthViewModel;
            var childCareForm = _childCareFormService.GetByUserId(Id) as ChildCareFormViewModel;
            other.OtherChildViewModel = new OtherChildViewModel()
                {
                    Children = otherChildren.ToList()
                };
            otherOther.OtherChildViewModel = new OtherChildViewModel()
                {
                    Children = otherChildrenOther.ToList()
                };

            var allPreexist = new AllPreexistingViewModel()
                {
                    PreexistingSupportViewModel = new PreexistingSupportViewModel()
                        {
                            PreexistingSupportList = preexistList
                        },
                        ChildViewModel = new ChildViewModel()                        
                };
            var allPreexistOther = new AllPreexistingViewModel()
                {
                    PreexistingSupportViewModel = new PreexistingSupportViewModel()
                        {
                            PreexistingSupportList = preexistListOther
                        },
                    ChildViewModel = new ChildViewModel()
                };

            var financial = new FinancialFormsCompleted()
                {
                    Income = income.UserId != 0,
                    IncomeOther = incomeOther.UserId != 0,
                    OtherChildren = other.UserId != 0,
                    OtherChildrenOther = otherOther.UserId != 0,
                    SocialSecurity = social.UserId != 0,
                    SocialSecurityOther = socialOther.UserId != 0,
                    Health = health.UserId != 0,
                    ChildCareForm = childCareForm.UserId != 0,
                };
            var model = new FinancialViewModel
                {
                    FinancialFormsCompleted = financial,
                    IncomeViewModel = income,
                    IncomeOtherViewModel = incomeOther,
                    ChildAllViewModel = new ChildAllViewModel()
                        {
                            ChildViewModel = children,
                            ChildFormViewModel = childForm as ChildFormViewModel
                        },
                    SocialSecurityViewModel = social,
                    SocialSecurityOtherViewModel = socialOther,
                    PreexistingSupportViewModel = allPreexist,
                    PreexistingSupportOtherViewModel = allPreexistOther,
                    OtherChildrenViewModel = other,
                    OtherChildrenOtherViewModel = otherOther,
                    HealthViewModel = health,
                    ChildCareFormViewModel = childCareForm,
                };
            return View(model);
        }

    }
}
