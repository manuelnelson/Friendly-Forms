﻿using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class HouseViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "House")]
        public int MaritalHouse { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string Address { get; set; }
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
            return new House()
                {
                    Address = Address,
                    Equity = Equity ?? 0,
                    MaritalHouse = MaritalHouse,
                    MoneyOwed = MoneyOwed ?? 0,
                    MortgageOwner = MortgageOwner,
                    RetailValue = RetailValue ?? 0,
                    Divide = Divide,
                    UserId = UserId
                };
        }
    }
}
