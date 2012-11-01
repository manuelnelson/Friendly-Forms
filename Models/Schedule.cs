using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Schedule : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int DetermineBeginDate { get; set; }
        public DateTime? BeginDate { get; set; }
        public string WeekendDayStart { get; set; }
        public string WeekendDayEnd { get; set; }
        public string FatherWeekendOther { get; set; }
        public string MotherWeekendOther { get; set; }
        public DateTime PickedUp { get; set; }
        public string PickupLocation { get; set; }
        public DateTime DroppedOff { get; set; }
        public string DropOffLocation { get; set; }
        public int FatherWeekend { get; set; }
        public int MotherWeekend { get; set; }
        public int Weekdays { get; set; }
        public DateTime? WeekdayPickup { get; set; }
        public string WeekdayPickupLocation { get; set; }
        public DateTime? WeekdayDropoff { get; set; }
        public string WeekdayDropoffLocation { get; set; }
        public int MondayParent { get; set; }
        public int TuesdayParent { get; set; }
        public int WednesdayParent { get; set; }
        public int ThursdayParent { get; set; }
        public int FridayParent { get; set; }
        public string AdditionalProvisions { get; set; }
        public IViewModel ConvertToModel()
        {
            return new ScheduleViewModel()
            {
                BeginDate = BeginDate.HasValue ? BeginDate.Value.ToString("MM/dd/yyyy") : "",                
                DetermineBeginDate = DetermineBeginDate,
                DropOffLocation = DropOffLocation,
                DroppedOff = DroppedOff.ToString("hh:mm"),
                FatherWeekend = FatherWeekend,
                MotherWeekend = MotherWeekend,
                PickedUp = PickedUp.ToString("hh:mm"),
                PickupLocation = PickupLocation,
                UserId = UserId,
                Weekdays = Weekdays,
                FatherWeekendOther = FatherWeekendOther,
                MotherWeekendOther = MotherWeekendOther,
                WeekendDayStart = WeekendDayStart,
                WeekendDayEnd = WeekendDayEnd,
                WeekdayDropoff = WeekdayDropoff.HasValue ? WeekdayDropoff.Value.ToString("hh:mm") : "",
                WeekdayDropoffLocation = WeekdayDropoffLocation,
                WeekdayPickup = WeekdayPickup.HasValue ? WeekdayPickup.Value.ToString("hh:mm") : "",                
                WeekdayPickupLocation = WeekdayPickupLocation,
                MondayParent = MondayParent,
                TuesdayParent = TuesdayParent,
                WednesdayParent = WednesdayParent,
                ThursdayParent = ThursdayParent,
                AdditionalProvisions = AdditionalProvisions,
                FridayParent = FridayParent
            };
        }
        public void Update(IFormEntity entity)
        {
            var update = (Schedule)entity;
            BeginDate = update.BeginDate;
            DetermineBeginDate = update.DetermineBeginDate;
            DropOffLocation = update.DropOffLocation;
            DroppedOff = update.DroppedOff;
            FatherWeekend = update.FatherWeekend;
            MotherWeekend = update.MotherWeekend;
            PickedUp = update.PickedUp;
            PickupLocation = update.PickupLocation;
            UserId = update.UserId;
            Weekdays = update.Weekdays;
            FatherWeekendOther = update.FatherWeekendOther;
            MotherWeekendOther= update.MotherWeekendOther;
            WeekendDayStart = update.WeekendDayStart;
            WeekendDayEnd = update.WeekendDayEnd;
            WeekdayDropoff = update.WeekdayDropoff;
            WeekdayDropoffLocation = update.WeekdayDropoffLocation;
            WeekdayPickup = update.WeekdayPickup;
            WeekdayPickupLocation = update.WeekdayPickupLocation;
            MondayParent = update.MondayParent;
            TuesdayParent = update.TuesdayParent;
            WednesdayParent = update.WednesdayParent;
            ThursdayParent = update.ThursdayParent;
            AdditionalProvisions = update.AdditionalProvisions;
            FridayParent = update.FridayParent;
        }
    }
}
