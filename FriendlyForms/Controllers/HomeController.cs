using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Contracts;
using FriendlyForms.Authentication;
using FriendlyForms.Models;
using Models;

namespace FriendlyForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParticipantService _participantService;
        private readonly IChildService _childService;
        private readonly IChildSupportService _childSupportService;
        private readonly IHolidayService _holidayService;
        private readonly ISpecialCircumstancesService _specialCircumstancesService;
        private readonly IChildFormService _childFormService;
        //
        // GET: /Forms/
        public HomeController(IParticipantService participantService, IChildService childService, IChildSupportService childSupportService, IHolidayService holidayService, ISpecialCircumstancesService specialCircumstancesService, IChildFormService childFormService )
        {
            _participantService = participantService;
            _childService = childService;
            _childSupportService = childSupportService;
            _holidayService = holidayService;
            _specialCircumstancesService = specialCircumstancesService;
            _childFormService = childFormService;
        }
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var userId = User.FriendlyIdentity().Id;
                var childForm = _childFormService.GetByUserId(userId);
                var children = _childService.GetByUserId(userId);
                var childSupport = _childSupportService.GetByUserId(userId);
                var participants = _participantService.GetByUserId(userId);
                var specialCircumstance = _specialCircumstancesService.GetByUserId(userId);
                if(childForm.UserId != 0 && children.Children.Any())
                {
                    var holidays = _holidayService.GetByChildId(children.Children.First().Id) ?? new Holiday();

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
                    HasChildren = childForm.UserId != 0,
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
