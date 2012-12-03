using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ExtraHolidayViewModel : IViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [StringLength(100)]
        [Display(Name="Holiday Name")]
        public string HolidayName { get; set; }
        [Required]
        [Display(Name = "Father")]
        public int HolidayFather { get; set; }
        [Required]
        [Display(Name = "Mother")]
        public int HolidayMother { get; set; }

        public List<ExtraHoliday> ExtraHolidays { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new ExtraHoliday()
            {
                Id = Id,
                ChildId = ChildId,
                HolidayFather = HolidayFather,
                HolidayMother = HolidayMother,
                HolidayName = HolidayName,
                UserId = UserId
            };
        }
    }
}
