using Models.Contract;

namespace Models
{
    public class County : IEntity
    {
        public long Id { get; set; }
        public string CountyName { get; set; }
    }
}
