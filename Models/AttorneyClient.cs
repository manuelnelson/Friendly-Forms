using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("AttorneyClients")]
    //Determines which attorneys are tied to clients
    public class AttorneyClient : IEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual long ClientUserId { get; set; }
        public virtual bool ChangeNotification { get; set; }
        public virtual bool PrintNotification { get; set; }
    }
}
