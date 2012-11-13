﻿using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using FriendlyForms.Authentication;
using FriendlyForms.Models;
using Models.ViewModels;

namespace FriendlyForms.Controllers
{
    public class HomeController : Controller
    {
                private readonly ICourtService _courtService;
        private readonly IParticipantService _participantService;
        private readonly IChildService _childService;
        private readonly IPrivacyService _privacyService;
        private readonly IInformationService _informationService;
        private readonly IDecisionsService _decisionsService;
        private readonly IExtraDecisionsService _extraDecisionsService;
        private readonly IResponsibilityService _responsibilityService;
        private readonly ICommunicationService _communicationService;
        private readonly IScheduleService _scheduleService;
        private readonly ICountyService _countyService;
        private readonly IHouseService _houseService;
        private readonly IRealEstateService _realEstateService;
        private readonly IVehicleService _vehicleService;
        private readonly IDebtService _debtService;
        private readonly IAssetService _assetService;
        private readonly IHealthService _healthService;
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

        //
        // GET: /Forms/
        public HomeController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IExtraDecisionsService extraDecisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService,ICountyService countyService,
            IHouseService houseService, IRealEstateService realEstateService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthService healthService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IExtraHolidayService extraHolidayService,
            IIncomeService incomeService, ISocialSecurityService socialSecurityService, IPreexistingSupportService preexistingSupportService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService, ISpecialCircumstancesService specialCircumstancesService, IOtherChildService otherChildService)
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
            _realEstateService = realEstateService;
            _vehicleService = vehicleService;
            _debtService = debtService;
            _assetService = assetService;
            _healthService = healthService;
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
        }
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var userId = User.FriendlyIdentity().UserId;
                var children = _childService.GetByUserId(userId);
                var childSupport = _childSupportService.GetByUserId(userId);
                var participants = _participantService.GetByUserId(userId);
                var specialCircumstance = _specialCircumstancesService.GetByUserId(userId);
                if(children.Children.Any())
                {
                    var holidays = _holidayService.GetByChildId(children.Children.First().Id);

                    var allViewModel = new AllFormsViewModel
                    {
                        HasChildren = children.Children.Any(),
                        IsDomesticDone = childSupport.UserId != 0,
                        IsStarterDone = participants.UserId != 0,
                        IsParentingDone = holidays.UserId != 0,
                        IsFinancial = specialCircumstance.UserId != 0
                    };
                    return View(allViewModel);                
                }
                var allFormsViewModel = new AllFormsViewModel
                {
                    HasChildren = children.Children.Any(),
                    IsDomesticDone = childSupport.UserId != 0,
                    IsStarterDone = participants.UserId != 0,
                    IsParentingDone = false,
                    IsFinancial = false
                };
                return View(allFormsViewModel);                
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
