using BusinessLogic.Models;
using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class ParentingPlanViewModel
    {
        public CourtViewModel CourtViewModel { get; set; }
        public ParticipantViewModel ParticipantViewModel { get; set; }
        public ChildAllViewModel ChildAllViewModel { get; set; }
        public PrivacyViewModel PrivacyViewModel { get; set; }
        public InformationViewModel InformationViewModel { get; set; }
        public AllDecisionsViewModel AllDecisionsViewModel { get; set; }
        public ResponsibilityViewModel ResponsibilityViewModel { get; set; }
        public CommunicationViewModel CommunicationViewModel { get; set; }
        public ScheduleViewModel ScheduleViewModel { get; set; }
        public AllHolidaysViewModel HolidayViewModel { get; set; }
        public FormsCompleted FormsCompleted { get; set; }
        //Only used for output form
        public PpOutputFormHelper PpOutputFormHelper { get; set; }
    }

    public class PpOutputFormHelper
    {
        public string Parent { get; set; }
        public string NonCustodyParent { get; set; }
        public string NonCustodyParentName { get; set; }
        public bool NonCustodyIsFather { get; set; }
        public string LegalCustodyPhrase { get; set; }
        public string CommunicationTypePhrase { get; set; }
    }

    public class FormsCompleted
    {
        public bool Participant { get; set; }
        public bool Children { get; set; }
        public bool Privacy { get; set; }
        public bool Information { get; set; }
        public bool Decisions { get; set; }
        public bool Responsibility { get; set; }
        public bool Communication { get; set; }
        public bool Schedule { get; set; }
        public bool Holiday { get; set; }
    }
}