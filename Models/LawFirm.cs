using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("LawFirms")]
    public class LawFirm : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Subscription { get; set; }
    }
}
