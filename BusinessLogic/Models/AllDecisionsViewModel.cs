using System.Collections.Generic;
using System.Runtime.Serialization;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Models
{
    [DataContract]
    public class AllDecisionsViewModel
    {
        [DataMember]
        public DecisionsViewModel DecisionsViewModel { get; set; }
        [DataMember]
        public ExtraDecisionsViewModel ExtraDecisionsViewModel { get; set; }
        [DataMember]
        public List<Decisions> ChildDecisions { get; set; }
        [DataMember]
        public List<ExtraDecisions> ChildExtraDecisions { get; set; } 
    }
}
