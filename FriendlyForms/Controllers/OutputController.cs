using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using FriendlyForms.Authentication;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;
using Pechkin;
using Pechkin.Synchronized;
using System.IO;

namespace FriendlyForms.Controllers
{
    public class OutputController : Controller
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
        private readonly IDeviationsService _deviationsService;
        private readonly IOtherChildService _otherChildService;
        private readonly IVehicleFormService _vehicleFormService;
        private readonly SynchronizedPechkin _synchronizedPechkin;
        //
        // GET: /Forms/
        public OutputController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IExtraDecisionsService extraDecisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService, ICountyService countyService,
            IHouseService houseService, IPropertyService propertyService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthInsuranceService healthInsuranceService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IExtraHolidayService extraHolidayService,
            IIncomeService incomeService, ISocialSecurityService socialSecurityService, IPreexistingSupportService preexistingSupportService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService, IDeviationsService deviationsService, IOtherChildService otherChildService, IVehicleFormService vehicleFormService)
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
            _deviationsService = deviationsService;
            _otherChildService = otherChildService;
            _vehicleFormService = vehicleFormService;
            // set it up using fluent notation
            var globalConfig = new GlobalConfig();
            globalConfig.SetPaperSize(PaperKind.Letter);
            var margins = new Margins(100, 100, 100, 100);
            globalConfig.SetMargins(margins);           
            _synchronizedPechkin = new SynchronizedPechkin(globalConfig);
        }

        //
        // GET: /Output/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Parenting()
        {
            var userId = User.FriendlyIdentity().Id;
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            var participants = _participantService.GetByUserId(userId) as ParticipantViewModel;
            var children = _childService.GetByUserId(userId);
            var privacy = _privacyService.GetByUserId(userId);
            var information = _informationService.GetByUserId(userId);
            var decisions = new List<Decisions>();
            var extraDecisions = new List<ExtraDecisions>();
            var holidays = new List<Holiday>();
            var extraHolidays = new List<ExtraHoliday>();
            if (children.Children.Any())
            {
                decisions.AddRange(children.Children.Select(child => _decisionsService.GetByChildId(child.Id)));
            }
            if (children.Children.Any())
            {
                foreach (var child in children.Children)
                {
                    var tempDecisions = _extraDecisionsService.GetByChildId(child.Id);
                    extraDecisions.AddRange(tempDecisions);
                }
            }
            if (children.Children.Any())
            {
                holidays.AddRange(children.Children.Select(child => _holidayService.GetByChildId(child.Id)));
            }
            if (children.Children.Any())
            {
                foreach (var child in children.Children)
                {
                    var tempHolidays = _extraHolidayService.GetByChildId(child.Id);
                    extraHolidays.AddRange(tempHolidays);
                }
            }
            var responsibility = _responsibilityService.GetByUserId(userId);
            var communication = _communicationService.GetByUserId(userId) as CommunicationViewModel;
            var schedule = _scheduleService.GetByUserId(userId);
            var allDecisions = new AllDecisionsViewModel
            {
                ChildDecisions = decisions,
                ChildExtraDecisions = extraDecisions
            };
            var allHolidays = new AllHolidaysViewModel
                {
                    ChildHolidays = holidays,
                    ExtraChildHolidays = extraHolidays
                };
            var counties = _countyService.GetAll();
            court.Counties = counties;

            var formsViewModel = new FormsCompleted();

            //Setup output form            
            var outputViewModel = new PpOutputFormHelper
                {
                    CustodyInformation = _participantService.GetCustodyInformation(participants)
                };
            
            //Communication
            var communicationTypes = new List<string>();
            if (communication.Telephone)
            {
                communicationTypes.Add("Telephone");
            }
            if (communication.Email)
            {
                communicationTypes.Add("Email");
            }
            if (communication.Other)
            {
                communicationTypes.Add(communication.OtherMethod);
            }
            outputViewModel.CommunicationTypePhrase = string.Join(",", communicationTypes);



            var childViewModel = new ParentingPlanViewModel
            {
                CourtViewModel = court,
                ParticipantViewModel = participants as ParticipantViewModel,
                ChildAllViewModel = new ChildAllViewModel()
                    {
                        ChildViewModel = children,
                    },
                PrivacyViewModel = privacy as PrivacyViewModel,
                InformationViewModel = information as InformationViewModel,
                AllDecisionsViewModel = allDecisions,
                ResponsibilityViewModel = responsibility as ResponsibilityViewModel,
                CommunicationViewModel = communication,
                ScheduleViewModel = schedule as ScheduleViewModel,
                HolidayViewModel = allHolidays,
                PpOutputFormHelper = outputViewModel,
                FormsCompleted = formsViewModel
            };
            return View(childViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PrintForm(string html)
        {
            var contentPath = Server.MapPath("~/Content/");
            var css = System.IO.File.ReadAllText(Path.Combine(contentPath, "pdf.css"));
            var fullHtml = string.Format(@"<!DOCTYPE html> <html> <head><style type=""text/css"">{0}</style></head><body><div id=""main-content"">{1}</div></body></html>", css, html);
            var config = new ObjectConfig();
            config.SetAllowLocalContent(true);                    
            config.SetPrintBackground(true);

            var userId = User.FriendlyIdentity().Id;
            var participants = _participantService.GetByUserId(userId) as ParticipantViewModel;
            //var court = _courtService.GetByUserId(userId) as CourtViewModel;

            if (Request.Url != null)
            {
                var headerUrl = Request.Url.AbsoluteUri.Replace(Request.Url.LocalPath, "") + "/Headers/Header/" + userId;
                config.Header.SetHtmlContent(headerUrl);
            }
            //config.Header.SetFont(new Font("Times New Roman", 9F, FontStyle.Underline));
            //config.Header.SetContentSpacing(40.0);
            //config.Header.SetLeftText("        " + participants.PlaintiffsName + "  v.  " + participants.DefendantsName + "        \r\n");
            //config.Header.SetRightText("CAF #" + court.CaseNumber);
            //config.Header.SetLineSeparator(true);            
            config.Footer.SetFont(new Font("Times New Roman", 8F, FontStyle.Regular));
            config.Footer.SetTexts(participants.PlaintiffsName + " Initials_______", @"[page] of [topage]", participants.DefendantsName + " Initials________");
            var pdfBuf = _synchronizedPechkin.Convert(config, fullHtml);                        
            return File(pdfBuf, "application/pdf", "form.pdf");

            ////TODO should be an easier way to just send buffer straight to 
            //var fs = new FileStream(fn, FileMode.Create); // write bytes to file
            //fs.Write(pdfBuf, 0, pdfBuf.Length);
            //fs.Close();

            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            //Response.WriteFile(path);
            //Response.End();
            //var myProcess = new Process {StartInfo = {FileName = fn}}; // open the file in viewer
            //myProcess.Start(); 
            //System.IO.File.WriteAllBytes(contentPath, pdfBuf);
        }

        [Authorize]
        public ActionResult DomesticMediation()
        {
            var userId = User.FriendlyIdentity().Id;
            var house = _houseService.GetByUserId(userId);
            var property = _propertyService.GetByUserId(userId);
            var debt = _debtService.GetByUserId(userId);
            var assets = _assetService.GetByUserId(userId);
            var health = _healthInsuranceService.GetByUserId(userId);
            var spousal = _spousalService.GetByUserId(userId);
            var taxes = _taxService.GetByUserId(userId);
            var support = _childSupportService.GetByUserId(userId);
            var vehicles = _vehicleService.GetByUserId(userId).ToList();
            var vehicleForm = _vehicleFormService.GetByUserId(userId);
            var participants = _participantService.GetByUserId(userId);
            var court = _courtService.GetByUserId(userId);
            var vehicleModel = new VehicleViewModel()
            {
                VehicleList = vehicles
            };
            var formsViewModel = new FormsCompletedDomestic()
            {
                AssetCompleted = assets.UserId != 0,
                RealEstateCompleted = property.UserId != 0,
                DebtCompleted = debt.UserId != 0,
                HealthCompleted = health.UserId != 0,
                SpousalCompleted = spousal.UserId != 0,
                TaxCompleted = taxes.UserId != 0,
                ChildCompleted = support.UserId != 0,
                VehicleCompleted = vehicles.Any()
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
                ParticipantsViewModel = participants as ParticipantViewModel,
                CourtViewModel = court as CourtViewModel,
                FormsCompleted = formsViewModel
            };
            return View(domesticModel);
        }

    }
}
