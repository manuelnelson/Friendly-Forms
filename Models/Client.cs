using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Clients")]
    public class Client : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int ClientUserId { get; set; }
    }
}
