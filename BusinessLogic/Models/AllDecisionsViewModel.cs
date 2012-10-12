using System.Collections.Generic;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Models
{
    public class AllDecisionsViewModel
    {
        public DecisionsViewModel DecisionsViewModel { get; set; }
        public ExtraDecisionsViewModel ExtraDecisionsViewModel { get; set; }
        public List<Decisions> ChildDecisions { get; set; }
        public List<ExtraDecisions> ChildExtraDecisions { get; set; } 
    }
}
