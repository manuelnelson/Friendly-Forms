using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Schedules")]
    public class Schedule : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }

        public int DetermineBeginDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public string WeekendDayStart { get; set; }
        public string WeekendDayEnd { get; set; }
        public string CustodianWeekendOther { get; set; }
        public string NonCustodianWeekendOther { get; set; }
        public string PickedUp { get; set; }
        public string PickupLocation { get; set; }
        public string DroppedOff { get; set; }
        public string DropOffLocation { get; set; }
        public int CustodianWeekend { get; set; }
        public int NonCustodianWeekend { get; set; }
        public int Weekdays { get; set; }
        public string WeekdayPickup { get; set; }
        public string WeekdayPickupLocation { get; set; }
        public string WeekdayDropoff { get; set; }
        public string WeekdayDropoffLocation { get; set; }
        public bool MondayParent { get; set; }
        public bool TuesdayParent { get; set; }
        public bool WednesdayParent { get; set; }
        public bool ThursdayParent { get; set; }
        public bool FridayParent { get; set; }
        public bool SaturdayParent { get; set; }
        public bool SundayParent { get; set; }
        public int AnyAdditionalProvisions { get; set; }
        public string AdditionalProvisions { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
