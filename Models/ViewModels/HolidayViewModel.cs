using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class HolidayViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long ChildId { get; set; }
        [Required]
        public bool FridayHoliday { get; set; }
        [Required]
        public bool MondayHoliday { get; set; }
        [Required]
        public int Thanksgiving { get; set; }
        [Display(Name = "Thanksgiving details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string ThanksgivingOther { get; set; }
        [Display(Name = "Thanksgiving Time")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        [Required]
        public string ThanksgivingTime { get; set; }
        [Required]
        [Display(Name="Winter break")]
        public int Christmas { get; set; }

        [Display(Name = "Winter Break Time")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string ChristmasTime { get; set; }

        [Display(Name = "Winter break details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string ChristmasOther { get; set; }
        
        [Required]
        [Display(Name = "Spring break")]
        public int SpringBreak { get; set; }
        [Display(Name = "Spring break details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string SpringOther { get; set; }

        [Required]
        [Display(Name = "Begin Summer")]
        public int SummerBeginDays { get; set; }
        [Required]
        [Display(Name = "Summer Begin Time")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage="Time must be in hh:mm am/pm format")]
        public string SummerBeginTime { get; set; }
        [Required]
        [Display(Name = "End Summer")]
        public int SummerEndDays { get; set; }
        [Required]
        [Display(Name = "Summer End Time")]
        [RegularExpression(@"^(1[012]|0[1-9]):[0-5][0-9](\s)?(am|pm|AM|PM)$", ErrorMessage = "Time must be in hh:mm am/pm format")]
        public string SummerEndTime { get; set; }
        [Display(Name = "Summer Details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string SummerDetails { get; set; }
        [Required]
        [Display(Name = "Fall break")]
        public int FallBreak { get; set; }
        [Display(Name = "Fall details")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string FallOther { get; set; }
        [Required]
        public int ChristmasFather { get; set; }
        [Required]
        public int ChristmasMother { get; set; }
        [Required]
        public int SpringBreakFather { get; set; }
        [Required]
        public int SpringBreakMother { get; set; }
        [Required]
        public int FallBreakFather { get; set; }
        [Required]
        public int FallBreakMother { get; set; }
        [Required]
        public int ThanksgivingFather { get; set; }
        [Required]
        public int ThanksgivingMother { get; set; }
        [Required]
        public int MlkFather { get; set; }
        [Required]
        public int MlkMother { get; set; }
        [Required]
        public int PresidentsFather { get; set; }
        [Required]
        public int PresidentsMother { get; set; }
        [Required]
        public int MothersFather { get; set; }
        [Required]
        public int MothersMother { get; set; }
        [Required]
        public int MemorialFather { get; set; }
        [Required]
        public int MemorialMother { get; set; }
        [Required]
        public int FathersFather { get; set; }
        [Required]
        public int FathersMother { get; set; }
        [Required]
        public int IndependenceFather { get; set; }
        [Required]
        public int IndependenceMother { get; set; }
        [Required]
        public int LaborFather { get; set; }
        [Required]
        public int LaborMother { get; set; }
        [Required]
        public int HalloweenFather { get; set; }
        [Required]
        public int HalloweenMother { get; set; }
        [Required]
        public int ChildrensFather { get; set; }
        [Required]
        public int ChildrensMother { get; set; }
        [Required]
        public int MothersBdayFather { get; set; }
        [Required]
        public int MothersBdayMother { get; set; }
        [Required]
        public int FathersBdayFather { get; set; }
        [Required]
        public int FathersBdayMother { get; set; }
        [Required]
        public int ReligiousFather { get; set; }
        [Required]
        public int ReligiousMother { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Holiday>();
        }
    }
}
