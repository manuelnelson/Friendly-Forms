using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class TaxViewModel : IViewModel 
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Taxes { get; set; }
        [Display(Name = "Tax Description")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        public string TaxDescription { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Tax()
                {
                    TaxDescription = TaxDescription,
                    Taxes = Taxes,
                    UserId = UserId
                };
        }
    }
}
