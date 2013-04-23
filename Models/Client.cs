using Models.Contract;

namespace Models
{
    public class Client : IEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ClientUserId { get; set; }
    }
}
