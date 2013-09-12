using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("AttorneyPages")]
    public class AttorneyPage : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long LawFirmId { get; set; }
        [Ignore]
        public LawFirm LawFirm { get; set; }
        public string PageName { get; set; }
    }
}
