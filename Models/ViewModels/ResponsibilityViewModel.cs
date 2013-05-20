using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class ResponsibilityViewModel : IViewModel 
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        [Display(Name = "Beginning visitation")]
        public int BeginningVisitation { get; set; }
        [Required]
        [Display(Name = "End visitation")]
        public int EndVisitation { get; set; }
        [Required]
        [Display(Name = "Transporation costs")]
        public int TransportationCosts { get; set; }

        [Display(Name = "Father percentage")]
        public double FatherPercentage { get; set; }
        [Display(Name = "Mother percentage")]
        public double MotherPercentage { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Father costs")]
        public string OtherDetails { get; set; }


        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Responsibility>();
        }
    }
}
