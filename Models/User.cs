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
        public long? LawFirmId { get; set; }
        public string Position { get; set; }
        //Date the recurring payments started
        public DateTime? RecurringDateStart { get; set; }
        //True if still making payments, false if subscription cancelled
        public bool RecurringActive { get; set; }
        public int? AmountId { get; set; }
        //Contains card key from payment depot when credit card is added
        public string CcInfoKey { get; set; }
        //Contains customer key from payment depot when credit card is added
        public string CustomerKey { get; set; }
    }
}
