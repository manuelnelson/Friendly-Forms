using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using FriendlyForms.Authentication;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;

namespace FriendlyForms.Controllers
{
    [Authorize]
    public class FormsController : Controller
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
        private readonly ISpecialCircumstancesService _specialCircumstancesService;
        private readonly IOtherChildService _otherChildService;
        private readonly IVehicleFormService _vehicleFormService;
        private readonly IAddendumService _addendumService;
        private readonly IClientService _clientService;
        //
        // GET: /Forms/
        public FormsController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService,ICountyService countyService,
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IIncomeService incomeService, ISocialSecurityService socialSecurityService, 
            IPreexistingSupportService preexistingSupportService, IOtherChildrenService otherChildrenService, ISpecialCircumstancesService specialCircumstancesService, IOtherChildService otherChildService, IVehicleFormService vehicleFormService, IChildFormService childFormService, IAddendumService addendumService, IClientService clientService)
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
            _specialCircumstancesService = specialCircumstancesService;
            _otherChildService = otherChildService;
            _vehicleFormService = vehicleFormService;
            _childFormService = childFormService;
            _addendumService = addendumService;
            _clientService = clientService;
        }
        
        [Authorize]
        public ActionResult Starter(int userId)
        {            
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var loggedInUserId = User.FriendlyIdentity().Id;
            if (userId != loggedInUserId || !_clientService.LawyerHasClient(loggedInUserId, userId))
            {
                //no authority to view the page. show error message
                return RedirectToAction("NotAuthorized", "Account");
            }
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            var participants = _participantService.GetByUserId(userId);
            var children = _childService.GetByUserId(userId);
            var childForm = _childFormService.GetByUserId(userId);
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
                StarterFormsCompleted = formsViewModel
            };
            starterViewModel.FormUserId = userId;
            return View(starterViewModel);
        }

        
        [Authorize]
        public ActionResult Parenting(int userId)
        {
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var loggedInUserId = User.FriendlyIdentity().Id;
            if (userId != loggedInUserId || !_clientService.LawyerHasClient(loggedInUserId, userId))
            {
                //no authority to view the page. show error message
                return RedirectToAction("NotAuthorized", "Account");
            }
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            var participants = _participantService.GetByUserId(userId) as ParticipantViewModel;
            var children = _childService.GetByUserId(userId);
            var childForm = _childFormService.GetByUserId(userId);
            var privacy = _privacyService.GetByUserId(userId);
            var decisions = _decisionsService.GetByUserId(userId);
            var information = _informationService.GetByUserId(userId);
            var responsibility = _responsibilityService.GetByUserId(userId);
            var communication = _communicationService.GetByUserId(userId);
            var schedule = _scheduleService.GetByUserId(userId) as ScheduleViewModel;
            var custodyInformation = _participantService.GetCustodyInformation(participants);
            if (schedule != null) schedule.NonCustodialParent = custodyInformation.NonCustodyParent;

            var holiday = children.Children.Any() ? _holidayService.GetByChildId(children.Children.First().Id) : new Holiday();
            var addendum = _addendumService.GetByUserId(userId);
            var allDecisions = new AllDecisionsViewModel();
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
            childViewModel.FormUserId = userId;
            return View(childViewModel);
        }

        [Authorize]
        public ActionResult DomesticMediation(int userId)
        {
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var loggedInUserId = User.FriendlyIdentity().Id;
            if (userId != loggedInUserId || !_clientService.LawyerHasClient(loggedInUserId, userId))
            {
                //no authority to view the page. show error message
                return RedirectToAction("NotAuthorized", "Account");
            }
            var house = _houseService.GetByUserId(userId);
            var property = _propertyService.GetByUserId(userId);            
            var debt = _debtService.GetByUserId(userId);
            var assets = _assetService.GetByUserId(userId);
            var health= _healthInsuranceService.GetByUserId(userId);
            var spousal = _spousalService.GetByUserId(userId);
            var taxes = _taxService.GetByUserId(userId);
            var participants = _participantService.GetByUserId(userId) as ParticipantViewModel;
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
            var support = _childSupportService.GetByUserId(userId);
            var vehicleForm = _vehicleFormService.GetByUserId(userId);
            var vehicles = _vehicleService.GetByUserId(userId).ToList();
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
            domesticModel.FormUserId = userId;
            return View(domesticModel);
        }

        [Authorize]
        public ActionResult Financial(int userId)
        {
            //only show form if userId is current user, or if curerent user is lawyer of userId
            var loggedInUserId = User.FriendlyIdentity().Id;
            if (userId != loggedInUserId || !_clientService.LawyerHasClient(loggedInUserId, userId))
            {
                //no authority to view the page. show error message
                return RedirectToAction("NotAuthorized", "Account");
            }
            var income = _incomeService.GetByUserId(userId);
            var incomeOther = _incomeService.GetByUserId(userId, isOtherParent:true);
            var social = _socialSecurityService.GetByUserId(userId);
            var socialOther = _socialSecurityService.GetByUserId(userId, isOtherParent:true);
            var preexistList = _preexistingSupportService.GetByUserId(userId);
            var preexistListOther = _preexistingSupportService.GetByUserId(userId, isOtherParent:true);
            var other = _otherChildrenService.GetByUserId(userId);
            var otherChildren = _otherChildService.GetChildrenByOtherChildrenId(other.Id);
            var otherOther = _otherChildrenService.GetByUserId(userId, isOtherParent: true);
            var otherChildrenOther = _otherChildService.GetChildrenByOtherChildrenId(otherOther.Id);
            var circumstance = _specialCircumstancesService.GetByUserId(userId);
            var circumstanceOther = _specialCircumstancesService.GetByUserId(userId, isOtherParent: true);

            other.OtherChildViewModel = new OtherChildViewModel()
                {
                    Children = otherChildren.ToList()
                };
            otherOther.OtherChildViewModel = new OtherChildViewModel()
                {
                    Children = otherChildrenOther.ToList()
                };
            var financial = new FinancialFormsCompleted()
                {
                    Income = income.UserId != 0,
                    IncomeOther = incomeOther.UserId != 0,
                    OtherChildren = other.UserId != 0,
                    OtherChildrenOther = otherOther.UserId != 0,
                    Preexisting = preexistList.Any(),
                    PreexistingOther = preexistListOther.Any(),
                    SocialSecurity = social.UserId != 0,
                    SocialSecurityOther = socialOther.UserId != 0,
                    SpecialCircumstance = circumstance.UserId != 0,
                    SpecialCircumstanceOther = circumstanceOther.UserId !=0
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

            var model = new AllFinancialViewModel()
                {
                    FinancialFormsCompleted = financial,
                    IncomeViewModel = income,
                    IncomeOtherViewModel = incomeOther,
                    SocialSecurityViewModel = social,
                    SocialSecurityOtherViewModel = socialOther,
                    PreexistingSupportViewModel = allPreexist,
                    PreexistingSupportOtherViewModel = allPreexistOther,
                    OtherChildrenViewModel = other,
                    OtherChildrenOtherViewModel = otherOther,
                    SpecialCircumstancesViewModel = circumstance,
                    SpecialCircumstancesOtherViewModel = circumstanceOther
                };
            model.FormUserId = userId;
            return View(model);
        }

    }
}
