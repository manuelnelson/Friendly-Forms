using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("AttorneyPageUsers")]
    public class AttorneyPageUser : IEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }

        public virtual long AttorneyPageId { get; set; }
        [Ignore]
        public virtual AttorneyPage AttorneyPage { get; set; }
    }
}
