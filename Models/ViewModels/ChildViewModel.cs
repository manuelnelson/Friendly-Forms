using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ChildViewModel : IViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Name { get; set; }
        
        [Display(Name = "Date of Birth")]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        [Required]
        public string DateOfBirth { get; set; }

        public int ChildFormId { get; set; }
        public List<Child> Children { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Child()
                {
                    DateOfBirth = string.IsNullOrEmpty(DateOfBirth) ? (DateTime?) null : Convert.ToDateTime(DateOfBirth),
                    Name = Name,
                    UserId = UserId,
                    Id = Id,
                    ChildFormId = ChildFormId
                };
        }
    }
}
