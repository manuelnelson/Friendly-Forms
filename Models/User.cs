using System;
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
        public bool Paid { get; set; }
        public int? LawFirmId { get; set; }
        public string Position { get; set; }
        public DateTime? RecurringDateStart { get; set; }
        //Contains card key from payment depot when credit card is added
        public string CcInfoKey { get; set; }
        //Contains customer key from payment depot when credit card is added
        public string CustomerKey { get; set; }
    }
}
