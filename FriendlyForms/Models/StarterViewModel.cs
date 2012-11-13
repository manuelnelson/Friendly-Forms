using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class StarterViewModel
    {
        public CourtViewModel CourtViewModel { get; set; }
        public ParticipantViewModel ParticipantViewModel { get; set; }
        public ChildrenViewModel ChildrenViewModel { get; set; }
        public StarterFormsCompleted StarterFormsCompleted { get; set; }
    }
    public class StarterFormsCompleted
    {
        public bool Participant { get; set; }
        public bool Children { get; set; }
    }
}