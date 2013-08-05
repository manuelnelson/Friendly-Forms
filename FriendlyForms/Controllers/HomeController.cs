using System;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using FriendlyForms.Models;
using Models;

namespace FriendlyForms.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IChildService _childService;
        private readonly IChildSupportService _childSupportService;
        private readonly IHolidayService _holidayService;
        private readonly IDeviationsService _deviationsService;
        private readonly IChildFormService _childFormService;
        private readonly IClientService _clientService;
        //
        // GET: /Forms/
        public HomeController(IParticipantService participantService, IChildService childService, IChildSupportService childSupportService, IHolidayService holidayService, IDeviationsService deviationsService, IChildFormService childFormService,IClientService clientService)
        {
            _participantService = participantService;
            _childService = childService;
            _childSupportService = childSupportService;
            _holidayService = holidayService;
            _deviationsService = deviationsService;
            _childFormService = childFormService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            //var user = UserSession;
            //ViewBag.IsAuthenticated = user.IsAuthenticated;
            //if (UserSession.IsAuthenticated)
            //{
            //    var userId = Convert.ToInt32(UserSession.CustomId);
            //    var children = _childService.GetByUserId(userId);
            //    var childForm = _childFormService.GetByUserId(userId);
            //    var childSupport = _childSupportService.GetByUserId(userId);
            //    var participants = _participantService.GetByUserId(userId);
            //    var specialCircumstance = _deviationsService.GetByUserId(userId);
            //    if(childForm.UserId != 0 && children.Children.Any())
            //    {
            //        var holidays = _holidayService.GetByChildId(children.Children.First().Id) ?? new Holiday();

            //        var allViewModel = new AllFormsViewModel
            //        {
            //            HasChildren = children.Children.Any(),
            //            IsDomesticDone = childSupport.UserId != 0,
            //            IsStarterDone = participants.UserId != 0,
            //            IsParentingDone = holidays.UserId != 0,
            //            IsFinancial = specialCircumstance.UserId != 0,
            //            FormUserId = userId
            //        };
            //        return View(allViewModel);                
            //    }
            //    var allFormsViewModel = new AllFormsViewModel
            //    {
            //        HasChildren = childForm.UserId != 0,
            //        IsDomesticDone = childSupport.UserId != 0,
            //        IsStarterDone = participants.UserId != 0,
            //        IsParentingDone = false,
            //        IsFinancial = false,
            //        FormUserId = userId
            //    };
            //    return View(allFormsViewModel);                
            //}
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
