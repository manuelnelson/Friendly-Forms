using BusinessLogic.Models;

namespace FriendlyForms.Models
{
    public class PpOutputFormHelper
    {
        public CustodyInformation CustodyInformation { get; set; }
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
        public bool Addendum { get; set; }
    }
}