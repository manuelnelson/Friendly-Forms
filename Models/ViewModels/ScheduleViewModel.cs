using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ScheduleViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        [Display(Name = "Determine begin date")]
        public int DetermineBeginDate { get; set; }
        [Display(Name = "Begin date")]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        public string BeginDate { get; set; }
        [Display(Name = "Custodian Weekend")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string CustodianWeekendOther { get; set; }
        [Display(Name = "Non-custodian Weekend")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string NonCustodianWeekendOther { get; set; }

        [Required]
        [Display(Name = "Weekend Start")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string WeekendDayStart { get; set; }
        [Required]
        [Display(Name = "Weekend End")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string WeekendDayEnd { get; set; }

        [Required]
        [Display(Name = "Pick up")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string PickedUp { get; set; }
        [Required]
        [Display(Name = "Pick up location")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string PickupLocation { get; set; }
        [Required]
        [Display(Name = "Drop off")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string DroppedOff { get; set; }
        [Required]
        [Display(Name = "Drop off location")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string DropOffLocation { get; set; }
        [Required]
        [Display(Name = "Custodian weekend")]
        public int CustodianWeekend { get; set; }
        [Required]
        [Display(Name = "Non-custodian weekend")]
        public int NonCustodianWeekend { get; set; }
        [Required]
        public int Weekdays { get; set; }
        [Display(Name = "Weekday pickup")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string WeekdayPickup { get; set; }
        [Display(Name = "Weekday pickup location")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string WeekdayPickupLocation { get; set; }
        [Display(Name = "Weekday drop off")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string WeekdayDropoff { get; set; }
        [Display(Name = "Weekday drop off location")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string WeekdayDropoffLocation { get; set; }
        [Display(Name = "Monday parent")]
        public bool MondayParent { get; set; }
        [Display(Name = "Tuesday parent")]
        public bool TuesdayParent { get; set; }
        [Display(Name = "Wednesday parent")]
        public bool WednesdayParent { get; set; }
        [Display(Name = "Thursday parent")]
        public bool ThursdayParent { get; set; }
        [Display(Name = "Friday parent")]
        public bool FridayParent { get; set; }
        [Display(Name = "Saturday parent")]
        public bool SaturdayParent { get; set; }
        [Display(Name = "Sunday parent")]
        public bool SundayParent { get; set; }
        [Display(Name = "Additional Provisions")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string AdditionalProvisions { get; set; }

        //This isn't in the model, just useful for the view
        public string NonCustodialParent { get; set; }
        public string CustodialParent { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Schedule
                {
                    BeginDate = string.IsNullOrEmpty(BeginDate) ? (DateTime?)null : Convert.ToDateTime(BeginDate),                    
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
                    WeekendDayEnd = WeekendDayEnd,
                    WeekendDayStart = WeekendDayStart,
                    WeekdayDropoff = WeekdayDropoff,                    
                    WeekdayDropoffLocation = WeekdayDropoffLocation,
                    WeekdayPickup = WeekdayPickup,                    
                    WeekdayPickupLocation = WeekdayPickupLocation,
                    MondayParent = MondayParent,
                    TuesdayParent = TuesdayParent,
                    WednesdayParent = WednesdayParent,
                    ThursdayParent = ThursdayParent,
                    FridayParent = FridayParent,
                    SaturdayParent = SaturdayParent,
                    SundayParent = SundayParent,                    
                    AdditionalProvisions = AdditionalProvisions
                };
        }
    }
}
