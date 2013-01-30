using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class StarterViewModel
    {
        public CourtViewModel CourtViewModel { get; set; }
        public ParticipantViewModel ParticipantViewModel { get; set; }
        public ChildAllViewModel ChildAllViewModel { get; set; }
        public StarterFormsCompleted StarterFormsCompleted { get; set; }
        public int FormUserId { get; set; }
    }
    public class StarterFormsCompleted
    {
        public bool Participant { get; set; }
        public bool Children { get; set; }
    }
}