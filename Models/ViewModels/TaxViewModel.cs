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
