using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("Counties")]
    public class County : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string CountyName { get; set; }
    }
}
