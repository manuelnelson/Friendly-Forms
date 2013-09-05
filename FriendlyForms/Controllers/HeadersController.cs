using System.Web.Mvc;
using BusinessLogic.Contracts;
using Models;

namespace FriendlyForms.Controllers
{
    public class HeadersController : Controller
    {
        private readonly ICourtService _courtService;
        private readonly IParticipantService _participantService;

        public HeadersController(ICourtService courtService, IParticipantService participantService)
        {
            _courtService = courtService;
            _participantService = participantService;
        }

        //
        // GET: /Headers/        
        public ActionResult Header(int id)
        {
            var court = _courtService.GetByUserId(id);
            var participants = _participantService.GetByUserId(id);
            var starter = new StarterViewModel()
            {
                Court = court as Court,
                Participant = participants as Participant
            };
            return View(starter);
        }
        public class StarterViewModel
        {
            public Court Court { get; set; }
            public Participant Participant { get; set; }
        }
    }
}
