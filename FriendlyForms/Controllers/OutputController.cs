﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using FriendlyForms.Authentication;
using FriendlyForms.Models;
using Models;
using Models.ViewModels;
using PdfSharp.Pdf.IO;
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
        private readonly IVehicleFormService _vehicleFormService;
        private readonly SynchronizedPechkin _synchronizedPechkin;
        //
        // GET: /Forms/
        public OutputController(ICourtService courtService, IParticipantService participantService, IChildService childService, IPrivacyService privacyService, IInformationService informationService, IDecisionsService decisionService, IExtraDecisionsService extraDecisionService, IResponsibilityService responsibilityService, ICommunicationService communicationService, IScheduleService scheduleService, ICountyService countyService,
            IHouseService houseService, IRealEstateService realEstateService, IVehicleService vehicleService, IDebtService debtService, IAssetService assetService, IHealthService healthService, ISpousalService spousalService, ITaxService taxService, IChildSupportService childSupportService, IHolidayService holidayService, IExtraHolidayService extraHolidayService,
            IIncomeService incomeService, ISocialSecurityService socialSecurityService, IPreexistingSupportService preexistingSupportService, IPreexistingSupportChildService preexistingSupportChildService, IOtherChildrenService otherChildrenService, ISpecialCircumstancesService specialCircumstancesService, IOtherChildService otherChildService, IVehicleFormService vehicleFormService)
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
            var userId = User.FriendlyIdentity().UserId;
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            var participants = _participantService.GetByUserId(userId);
            var children = _childService.GetByUserId(userId);
            var privacy = _privacyService.GetByUserId(userId);
            var information = _informationService.GetByUserId(userId);
            var decisions = new List<Decisions>();
            var extraDecisions = new List<ExtraDecisions>();
            var holidays = new List<Holiday>();
            var extraHolidays = new List<ExtraHoliday>();
            if (children.Children.Any())
            {
                decisions.AddRange(children.Children.Select(child => (Decisions)_decisionsService.GetByChildId(child.Id).ConvertToEntity()));
            }
            if (children.Children.Any())
            {
                foreach (var child in children.Children)
                {
                    var tempDecisions = _extraDecisionsService.GetByChildId(child.Id).ExtraDecisions;
                    extraDecisions.AddRange(tempDecisions);
                }
            }
            if (children.Children.Any())
            {
                holidays.AddRange(children.Children.Select(child => (Holiday)_holidayService.GetByChildId(child.Id).ConvertToEntity()));
            }
            if (children.Children.Any())
            {
                foreach (var child in children.Children)
                {
                    var tempHolidays = _extraHolidayService.GetByChildId(child.Id).ExtraHolidays;
                    extraHolidays.AddRange(tempHolidays);
                }
            }
            var responsibility = _responsibilityService.GetByUserId(userId);
            var communication = _communicationService.GetByUserId(userId);
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
            var childViewModel = new ParentingPlanViewModel
            {
                CourtViewModel = court,
                ParticipantViewModel = participants as ParticipantViewModel,
                ChildAllViewModel = new ChildAllViewModel()
                    {
                        ChildrenViewModel = children,
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PrintForm(string html)
        {
            var contentPath = Server.MapPath("~/Content/");
            var css = System.IO.File.ReadAllText(Path.Combine(contentPath, "pdf.css"));
            //var css = @"#main-content hr{border-color:red;} h4{font-size: 60px};";
            var fullHtml = string.Format(@"<!DOCTYPE html> <html> <head><style type=""text/css"">{0}</style></head><body><div id=""main-content"">{1}</div></body></html>", css, html);
            var config = new ObjectConfig();
            config.SetAllowLocalContent(true)                    
                  .SetPrintBackground(true);

            var userId = User.FriendlyIdentity().UserId;
            var participants = _participantService.GetByUserId(userId) as ParticipantViewModel;
            var court = _courtService.GetByUserId(userId) as CourtViewModel;
            //var htmlText = @"<span class='pull-left'>" + participants.PlaintiffsName + " v. " +
            //               participants.DefendantsName + "</span><span class='pull-right'>CAF #" + court.CaseNumber +
            //               "</span>";
            config.Footer.SetFont(new Font("Times New Roman", 12F, FontStyle.Regular));
            config.Header.SetFont(new Font("Times New Roman", 12F, FontStyle.Underline));
            config.Header.SetLeftText("        " + participants.PlaintiffsName + "v. " + participants.DefendantsName + "        ");
            config.Header.SetRightText("CAF #" + court.CaseNumber);
            config.Footer.SetTexts("Plaintiff Initials_______", @"[page] of [topage]", "Defendant Initials________");
            var pdfBuf = _synchronizedPechkin.Convert(config, fullHtml);            
            //Response.AppendHeader("Content-Disposition", "inline;form.pdf");
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
            var userId = User.FriendlyIdentity().UserId;
            var house = _houseService.GetByUserId(userId);
            var property = _realEstateService.GetByUserId(userId);
            var debt = _debtService.GetByUserId(userId);
            var assets = _assetService.GetByUserId(userId);
            var health = _healthService.GetByUserId(userId);
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
                RealEstateViewModel = property as RealEstateViewModel,
                VehicleAllViewModel = new VehicleAllViewModel()
                    {
                      VehicleViewModel = vehicleModel,
                      VehicleFormViewModel = vehicleForm as VehicleFormViewModel
                    },
                DebtViewModel = debt as DebtViewModel,
                AssetViewModel = assets as AssetViewModel,
                HealthViewModel = health as HealthViewModel,
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
