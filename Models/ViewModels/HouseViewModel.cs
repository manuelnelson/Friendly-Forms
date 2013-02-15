using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class HouseViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "House")]
        public int MaritalHouse { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string Address { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string CityState { get; set; }
        [RegularExpression(pattern: @"^(\d{5}|\d{4})(?:[-\s]\d{4})?$", ErrorMessage = @"Must be a valid zip code")]        
        public string ZipCode { get; set; }

        [Display(Name = "Retail Value")]
        public int? RetailValue { get; set; }
        [Display(Name = "Money owed")]
        public int? MoneyOwed { get; set; }
        public int? Equity { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Mortgage owner")]
        [StringLength(100)]
        public string MortgageOwner { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Divide { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<House>();
        }
    }
}
