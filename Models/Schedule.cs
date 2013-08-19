﻿using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Schedules")]
    public class Schedule : IEntity, IFormEntity
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
        public string AdditionalProvisions { get; set; }
        public IViewModel ConvertToModel()
        {
            return new ScheduleViewModel
                {
                BeginDate = BeginDate.HasValue ? BeginDate.Value.ToString("MM/dd/yyyy") : "",                
                DetermineBeginDate = DetermineBeginDate,
                DropOffLocation = DropOffLocation,
                DroppedOff = DroppedOff,
                CustodianWeekend = CustodianWeekend,
                NonCustodianWeekend = NonCustodianWeekend,
                PickedUp = PickedUp,
                PickupLocation = PickupLocation,
                UserId = UserId,
                Weekdays = Weekdays,
                CustodianWeekendOther = CustodianWeekendOther,
                NonCustodianWeekendOther = NonCustodianWeekendOther,
                WeekendDayStart = WeekendDayStart,
                WeekendDayEnd = WeekendDayEnd,
                WeekdayDropoff = WeekdayDropoff,
                WeekdayDropoffLocation = WeekdayDropoffLocation,
                WeekdayPickup = WeekdayPickup,                
                WeekdayPickupLocation = WeekdayPickupLocation,
                MondayParent = MondayParent,
                TuesdayParent = TuesdayParent,
                WednesdayParent = WednesdayParent,
                ThursdayParent = ThursdayParent,
                AdditionalProvisions = AdditionalProvisions,
                FridayParent = FridayParent
            };
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var update = (Schedule)entity;
            BeginDate = update.BeginDate;
            DetermineBeginDate = update.DetermineBeginDate;
            DropOffLocation = update.DropOffLocation;
            DroppedOff = update.DroppedOff;
            CustodianWeekend = update.CustodianWeekend;
            NonCustodianWeekend = update.NonCustodianWeekend;
            PickedUp = update.PickedUp;
            PickupLocation = update.PickupLocation;
            UserId = update.UserId;
            Weekdays = update.Weekdays;
            CustodianWeekendOther = update.CustodianWeekendOther;
            NonCustodianWeekendOther= update.NonCustodianWeekendOther;
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
