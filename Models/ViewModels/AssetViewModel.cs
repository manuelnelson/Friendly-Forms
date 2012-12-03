﻿using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class AssetViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int Retirement { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Details")]
        public string RetirementDescription { get; set; }
        
        [Required]
        [Display(Name = "Non-Retirement")]
        public int NonRetirement { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Non-Retirement Details")]
        public string NonRetirementDescription { get; set; }
        
        [Required]
        public int Business { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Business Description")]
        public string BusinessDescription { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Other Assets")]
        public string AdditionalAssets { get; set; }


        public IFormEntity ConvertToEntity()
        {
            return new Assets()
                {
                    Business = Business,
                    BusinessDescription = BusinessDescription,
                    NonRetirement = NonRetirement,
                    NonRetirementDescription = NonRetirementDescription,
                    Retirement = Retirement,
                    RetirementDescription = RetirementDescription,
                    AdditionalAssets = AdditionalAssets,
                    UserId = UserId
                };
        }
    }
}
