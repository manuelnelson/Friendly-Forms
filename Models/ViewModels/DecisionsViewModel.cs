﻿using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class DecisionsViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long ChildId { get; set; }
        [Required]
        public int Education { get; set; }
        [Required]
        [Display(Name = "Health Care")]
        public int HealthCare { get; set; }
        [Required]
        public int Religion { get; set; }
        [Required]
        [Display(Name = "Extra Curricular")]
        public int ExtraCurricular { get; set; }

        [Display(Name = "Resolve")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string BothResolve { get; set; }
        
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Decisions>();
        }
    }
}
