using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ScheduleViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Determine begin date")]
        public int DetermineBeginDate { get; set; }
        [Display(Name = "Begin date")]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        public string BeginDate { get; set; }
        [Required]
        [Display(Name = "Father Weekend")]
        public string FatherWeekendOther { get; set; }
        [Display(Name = "Mother Weekend")]
        public string MotherWeekendOther { get; set; }

        [Required]
        [Display(Name = "Weekend Start")]
        public string WeekendDayStart { get; set; }
        [Required]
        [Display(Name = "Weekend End")]
        public string WeekendDayEnd { get; set; }

        [Required]
        [Display(Name = "Pick up")]
        public string PickedUp { get; set; }
        [Required]
        [Display(Name = "Pick up location")]
        public string PickupLocation { get; set; }
        [Required]
        [Display(Name = "Drop off")]
        public string DroppedOff { get; set; }
        [Required]
        [Display(Name = "Drop off location")]
        public string DropOffLocation { get; set; }
        [Required]
        [Display(Name = "Father weekend")]
        public int FatherWeekend { get; set; }
        [Required]
        [Display(Name = "Mother weekend")]
        public int MotherWeekend { get; set; }
        [Required]
        public int Weekdays { get; set; }
        [Display(Name = "Weekday pickup")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string WeekdayPickup { get; set; }
        [Display(Name = "Weekday pickup location")]
        public string WeekdayPickupLocation { get; set; }
        [Display(Name = "Weekday drop off")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string WeekdayDropoff { get; set; }
        [Display(Name = "Weekday drop off location")]
        public string WeekdayDropoffLocation { get; set; }
        [Display(Name = "Monday parent")]
        public int? MondayParent { get; set; }
        [Display(Name = "Tuesday parent")]
        public int? TuesdayParent { get; set; }
        [Display(Name = "Wednesday parent")]
        public int? WednesdayParent { get; set; }
        [Display(Name = "Thursday parent")]
        public int? ThursdayParent { get; set; }
        [Display(Name = "Friday parent")]
        public int? FridayParent { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Schedule()
                {
                    BeginDate = string.IsNullOrEmpty(BeginDate) ? (DateTime?)null : Convert.ToDateTime(BeginDate),                    
                    DetermineBeginDate = DetermineBeginDate,
                    DropOffLocation = DropOffLocation,
                    DroppedOff = Convert.ToDateTime(DroppedOff),
                    FatherWeekend = FatherWeekend,
                    MotherWeekend = MotherWeekend,
                    PickedUp = Convert.ToDateTime(PickedUp),
                    PickupLocation = PickupLocation,
                    UserId = UserId,
                    Weekdays = Weekdays,
                    FatherWeekendOther = FatherWeekendOther,
                    MotherWeekendOther = MotherWeekendOther,
                    WeekendDayEnd = WeekendDayEnd,
                    WeekendDayStart = WeekendDayStart,
                    WeekdayDropoff = string.IsNullOrEmpty(WeekdayDropoff) ? (DateTime?)null : Convert.ToDateTime(WeekdayDropoff),                    
                    WeekdayDropoffLocation = WeekdayDropoffLocation,
                    WeekdayPickup = string.IsNullOrEmpty(WeekdayPickup) ? (DateTime?)null : Convert.ToDateTime(WeekdayPickup),                    
                    WeekdayPickupLocation = WeekdayPickupLocation,
                    MondayParent = MondayParent ?? 0,
                    TuesdayParent = TuesdayParent ?? 0,
                    WednesdayParent = WednesdayParent ?? 0,
                    ThursdayParent = ThursdayParent ?? 0,
                    FridayParent = FridayParent ?? 0                    
                };
        }
    }
}
