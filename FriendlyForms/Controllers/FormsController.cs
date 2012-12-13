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
        private readonly IExtraDecisionsService _extraDecisionsService;
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
        private readonly IExtraHolidayService _extraHolidayService;
        private readonly IIncomeService _incomeService;
        private readonly ISocialSecurityService _socialSecurityService;
        private readonly IPreexistingSupportService _preexistingSupportService;
        private readonly IPreexistingSupportChildService _preexistingSupportChildService;
        private readonly IOtherChildrenService _otherChildrenService;
        private readonly ISpecialCircumstancesService _specialCircumstancesService;
        private readonly IOtherChildService _otherChildService;
        private readonly IVehicleFormService _vehicleFormService;
        //
        // GET: /Forms/
        public FormsController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IExtraDecisionsService extraDecisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService,ICountyService countyService,
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IExtraHolidayService extraHolidayService,
            IIncomeService incomeService, ISocialSecurityService socialSecurityService, IPreexistingSupportService preexistingSupportService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService, ISpecialCircumstancesService specialCircumstancesService, IOtherChildService otherChildService, IVehicleFormService vehicleFormService, IChildFormService childFormService)
        {
            _courtService = courtService;
            _participantService = participantService;
            _childService = childService;
            _privacyService = privacyService;
            _informationService = informationService;
            _decisionsService = decisionService;
            _extraDecisionsService = extraDecisionService;
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
            _extraHolidayService = extraHolidayService;
            _incomeService = incomeService;
            _socialSecurityService = socialSecurityService;
            _preexistingSupportService = preexistingSupportService;
            _preexistingSupportChildService = preexistingSupportChildService;
            _otherChildrenService = otherChildrenService;
            _specialCircumstancesService = specialCircumstancesService;
            _otherChildService = otherChildService;
            _vehicleFormService = vehicleFormService;
            _childFormService = childFormService;
        }
        
        public ActionResult Starter()
        {
            var userId = User.FriendlyIdentity().UserId;
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
            return View(starterViewModel);
        }

        
        
        public ActionResult Parenting()
        {
            var userId = User.FriendlyIdentity().UserId;
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            var participants = _participantService.GetByUserId(userId);
            var children = _childService.GetByUserId(userId);
            var childForm = _childFormService.GetByUserId(userId);
            var privacy = _privacyService.GetByUserId(userId);
            var decisions = _decisionsService.GetByUserId(userId);
            var information = _informationService.GetByUserId(userId);
            var responsibility = _responsibilityService.GetByUserId(userId);
            var communication = _communicationService.GetByUserId(userId);
            var schedule = _scheduleService.GetByUserId(userId);
            var holiday = children.Children.Any() ? _holidayService.GetByChildId(children.Children.First().Id) : new Holiday();
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
                    Holiday = holiday.UserId != 0,
                    Information = information.UserId != 0,
                    Participant = participants.UserId != 0,
                    Privacy = privacy.UserId != 0,
                    Responsibility = responsibility.UserId != 0,
                    Schedule = schedule.UserId != 0
                };

            var childViewModel = new ParentingPlanViewModel
                {
                    CourtViewModel = court,
                    ParticipantViewModel = participants as ParticipantViewModel,
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
                    ScheduleViewModel = schedule as ScheduleViewModel,
                    HolidayViewModel = allHolidays,
                    FormsCompleted = formsViewModel
                };
            return View(childViewModel);
        }

        //[HttpPost]
        //public JsonResult Court(CourtViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId; 
        //    _courtService.AddOrUpdate(model);
        //    return Json("success!");
        //}

        //[HttpPost]
        //public JsonResult Participants(ParticipantViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _participantService.AddOrUpdate(model);
        //    return Json("success!");
        //}
        
        //[HttpPost]
        //public JsonResult Children(ChildViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var child = _childService.AddOrUpdate(model);
        //    if (child.DateOfBirth != null)
        //    {
        //        var childFormatted = new 
        //            {
        //                Name = child.Name,
        //                DateOfBirth = child.DateOfBirth.Value.ToString("MM/dd/yyyy"),
        //                Id = child.Id
        //            };
        //        return Json(childFormatted, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var childFormatted = new
        //        {
        //            Name = child.Name,
        //            DateOfBirth = "Not provided",
        //            Id = child.Id
        //        };
        //        return Json(childFormatted, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public JsonResult ChildForm(ChildFormViewModel model)
        //{
        //    //throw new HttpException(404, "Unable to find the resource");
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var result = _childFormService.AddOrUpdate(model);
        //    return Json(result.Id);
        //}

        //[HttpPost]
        //public JsonResult Privacy(PrivacyViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _privacyService.AddOrUpdate(model);            
        //    return Json("Success!", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Information(InformationViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;            
        //    _informationService.AddOrUpdate(model);
        //    return Json("Success!", JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetChildDecision(int Id)
        //{
        //    var decisions = _decisionsService.GetByChildId(Id).ConvertToEntity();            
        //    var extraDecisions = _extraDecisionsService.GetByChildId(Id).ExtraDecisions;
        //    var allDecisions = new
        //        {
        //            Decisions = decisions,
        //            ExtraDecisions = extraDecisions
        //        };
        //    return Json(allDecisions, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetChildHoliday(int Id)
        //{
        //    var holidays = _holidayService.GetByChildId(Id).ConvertToEntity();
        //    var extraHolidays = _extraHolidayService.GetByChildId(Id).ExtraHolidays;
        //    var allHolidays = new
        //    {
        //        Holidays = holidays,
        //        ExtraHolidays = extraHolidays
        //    };
        //    return Json(allHolidays, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Decisions(DecisionsViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _decisionsService.AddOrUpdate(model);
        //    return Json("Success!", JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult ExtraDecisions(ExtraDecisionsViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var decision = _extraDecisionsService.AddOrUpdate(model);
        //    return Json(decision);
        //}

        //[HttpPost]
        //public JsonResult Responsibility(ResponsibilityViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _responsibilityService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Communication(CommunicationViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _communicationService.AddOrUpdate(model);
        //    return Json("Success!");
        //}

        //[HttpPost]
        //public JsonResult Schedule(ScheduleViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _scheduleService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Holidays(HolidayViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var holiday = _holidayService.AddOrUpdate(model);
        //    return Json(holiday);
        //}
        //[HttpPost]
        //public JsonResult ExtraHolidays(ExtraHolidayViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var extraHoliday = _extraHolidayService.AddOrUpdate(model);
        //    return Json(extraHoliday);
        //}

        public ActionResult DomesticMediation()
        {
            var userId = User.FriendlyIdentity().UserId;
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
            return View(domesticModel);
        }

        //[HttpPost]
        //public JsonResult MaritalHouse(HouseViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _houseService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Property(PropertyViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _propertyService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Debt(DebtViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _debtService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Vehicles(VehicleViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId; 
        //    var vehicleadded = _vehicleService.AddOrUpdate(model);
        //    return Json(vehicleadded);
        //}
        //[HttpPost]
        //public JsonResult VehicleForm(VehicleFormViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    var result = _vehicleFormService.AddOrUpdate(model);
        //    return Json(result.Id);
        //}
        //[HttpPost]
        //public JsonResult Assets(AssetViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _assetService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult HealthInsurance(HealthInsuranceViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _healthInsuranceService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult SpousalSupport(SpousalViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _spousalService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Taxes(TaxViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _taxService.AddOrUpdate(model);
        //    return Json("Success!");
        //}
        //[HttpPost]
        //public JsonResult Support(ChildSupportViewModel model)
        //{
        //    model.UserId = User.FriendlyIdentity().UserId;
        //    _childSupportService.AddOrUpdate(model);
        //    return Json("Success!");
        //}

        public ActionResult Financial()
        {
            var userId = User.FriendlyIdentity().UserId;
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

            return View(model);
        }
        [HttpPost]
        public JsonResult Income(IncomeViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            _incomeService.AddOrUpdate(model);
            return Json("Success!");
        }
        [HttpPost]
        public JsonResult SocialSecurity(SocialSecurityViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            _socialSecurityService.AddOrUpdate(model);
            return Json("Success!");
        }

        [HttpPost]
        public JsonResult AddSupport(PreexistingSupportViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            model.Support = 1;
            var preexist = _preexistingSupportService.AddOrUpdate(model);
            var formattedPrexist = new
                {
                    preexist.CourtName,
                    preexist.CaseNumber,
                    preexist.Monthly,
                    preexist.Id,
                    OrderDate = preexist.OrderDate.ToString("MM/dd/yyyy")
                };
            return Json(formattedPrexist);            
        }

        [HttpPost]
        public JsonResult GetChildSupport(int Id)
        {
            var childrenList = _preexistingSupportChildService.GetChildrenBySupportId(Id);
            var children = new List<object>();
            foreach (var child in childrenList)
            {
                if (child.DateOfBirth != null)
                {
                    var childFormatted = new
                        {
                            Name = child.Name,
                            DateOfBirth = child.DateOfBirth.Value.ToString("MM/dd/yyyy"),
                            Id = child.Id
                        };
                    children.Add(childFormatted);
                }
                else
                {
                    var childFormatted = new
                        {
                            Name = child.Name,
                            DateOfBirth = child.DateOfBirth.Value.ToString("MM/dd/yyyy"),
                            Id = child.Id
                        };
                    children.Add(childFormatted);
                }
            }
            return Json(children);
        }
        [HttpPost]
        public JsonResult AddChildSupport(PreexistingSupportChildViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            var child = _preexistingSupportChildService.AddOrUpdate(model);
            if (child.DateOfBirth != null)
            {
                var childFormatted = new
                {
                    Name = child.Name,
                    DateOfBirth = child.DateOfBirth.Value.ToString("MM/dd/yyyy"),
                    Id = child.Id
                };
                return Json(childFormatted);
            }
            else
            {
                var childFormatted = new
                {
                    Name = child.Name,
                    DateOfBirth = "Not provided",
                    Id = child.Id
                };
                return Json(childFormatted);
            }
        }

        [HttpPost]
        public JsonResult OtherChildren(OtherChildrenViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            var other = _otherChildrenService.AddOrUpdate(model);
            return Json(other);
        }

        [HttpPost]
        public JsonResult AddOtherChild(OtherChildViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            var child = _otherChildService.AddOrUpdate(model);
            var childFormatted = new
            {
                Name = child.Name,
                DateOfBirth = child.DateOfBirth.HasValue ? child.DateOfBirth.Value.ToString("MM/dd/yyyy"): "Not Provided",
                Id = child.Id
            };
            return Json(childFormatted);                        
        }

        [HttpPost]
        public JsonResult Circumstance(SpecialCircumstancesViewModel model)
        {
            model.UserId = User.FriendlyIdentity().UserId;
            _specialCircumstancesService.AddOrUpdate(model);
            return Json("Success!");
        }

    }
}
