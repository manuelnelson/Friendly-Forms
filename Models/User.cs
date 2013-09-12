using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("Users")]
    public class User : IEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public int UserAuthId { get; set; }
        public bool Verified { get; set; }
        public int? LawFirmId { get; set; }
        public string Position { get; set; }
    }
}
