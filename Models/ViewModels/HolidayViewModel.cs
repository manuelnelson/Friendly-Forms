using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class HolidayViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        public bool FridayHoliday { get; set; }
        [Required]
        public bool MondayHoliday { get; set; }
        [Required]
        public int Thanksgiving { get; set; }
        [Display(Name = "Thanksgiving details")]
        public string ThanksgivingOther { get; set; }
        [Required]
        public int Christmas { get; set; }

        [Display(Name = "Christmas Time")]
        public string ChristmasTime { get; set; }

        [Display(Name = "Christmas details")]
        public string ChristmasOther { get; set; }
        
        [Required]
        [Display(Name = "Spring break")]
        public int SpringBreak { get; set; }
        [Display(Name = "Spring break details")]
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
        public string SummerDetails { get; set; }
        [Required]
        [Display(Name = "Fall break")]
        public int FallBreak { get; set; }
        [Display(Name = "Fall details")]
        public string FallOther { get; set; }
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
            return new Holiday()
                {
                    ChildId = ChildId,
                    ChildrensFather = ChildrensFather,
                    ChildrensMother = ChildrensMother,
                    Christmas = Christmas,
                    ChristmasOther = ChristmasOther,
                    ChristmasTime = ChristmasTime,
                    FallBreak = FallBreak,
                    FallOther = FallOther,
                    FathersBdayFather = FathersBdayFather,
                    FathersBdayMother = FathersBdayMother,
                    FathersFather = FathersFather,
                    FathersMother = FathersMother,
                    FridayHoliday = FridayHoliday,
                    HalloweenFather = HalloweenFather,
                    HalloweenMother = HalloweenMother,
                    IndependenceFather = IndependenceFather,
                    IndependenceMother = IndependenceMother,
                    LaborFather = LaborFather,
                    LaborMother = LaborMother,
                    MemorialFather = MemorialFather,
                    MemorialMother = MemorialMother,
                    MlkFather = MlkFather,
                    MlkMother = MlkMother,
                    MondayHoliday = MondayHoliday,
                    MothersBdayFather = MothersBdayFather,
                    MothersBdayMother = MothersBdayMother,
                    MothersFather = MothersFather,
                    MothersMother = MothersMother,
                    PresidentsFather = PresidentsFather,
                    PresidentsMother = PresidentsMother,
                    ReligiousFather = ReligiousFather,
                    ReligiousMother = ReligiousMother,
                    SpringBreak = SpringBreak,
                    SpringOther = SpringOther,
                    SummerBeginDays = SummerBeginDays,
                    SummerBeginTime = SummerBeginTime,
                    SummerEndDays = SummerEndDays,
                    SummerEndTime = SummerEndTime,
                    SummerDetails = SummerDetails,
                    Thanksgiving = Thanksgiving,
                    ThanksgivingOther = ThanksgivingOther,
                    UserId = UserId
                };
        }
    }
}
