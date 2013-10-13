using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("States")]
    public class State : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}
