using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("AttorneyClients")]
    public class AttorneyClient : IEntity
    {
        public long Id { get; set; }
        //This is the attorney's UserId
        public long UserId { get; set; }
        [Ignore]
        public User User { get; set; }
        public long ClientUserId { get; set; }
    }
}
