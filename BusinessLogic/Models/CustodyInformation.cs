using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class CustodyInformation
    {
        [DataMember]
        public string CustodyParent { get; set; }
        [DataMember]
        public string NonCustodyParent { get; set; }
        [DataMember]
        public bool NonCustodyIsFather { get; set; }
        [DataMember]
        public string NonCustodyParentName { get; set; }
        [DataMember]
        public string CustodyParentName { get; set; }
        [DataMember]
        public string LegalCustodyPhrase { get; set; }
        [DataMember]
        public List<string> CustodianNames { get; set; }
    }
}
