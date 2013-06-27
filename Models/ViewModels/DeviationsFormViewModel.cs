﻿using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class DeviationsFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsOtherParent { get; set; }
        public int Deviation { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Unjust { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string BestInterest { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Impair { get; set; }
        public int? HighLow { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? LowDeviation { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string WhyLow { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? HighIncome { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? HighDeviation { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public int? NonSpecific { get; set; }
        public int? SpecificDeviations { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<DeviationsForm>();
        }

    }
}
