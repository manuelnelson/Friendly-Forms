namespace BusinessLogic.Models
{
    public class CustodyInformation
    {
        public string CustodyParent { get; set; }
        public string NonCustodyParent { get; set; }
        public bool NonCustodyIsFather { get; set; }
        public string NonCustodyParentName { get; set; }
        public string CustodyParentName { get; set; }
        public string LegalCustodyPhrase { get; set; }
    }
}
