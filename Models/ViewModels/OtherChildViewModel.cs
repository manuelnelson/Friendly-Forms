
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class OtherChildViewModel : IViewModel
    {
        public long Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        public string DateOfBirth { get; set; }

        public int OtherChildrenId { get; set; }

        public List<OtherChild> Children { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new OtherChild()
            {
                Id = Id,
                DateOfBirth = string.IsNullOrEmpty(DateOfBirth) ? (DateTime?)null : Convert.ToDateTime(this.DateOfBirth),
                Name = this.Name,
                UserId = this.UserId,
                OtherChildrenId = OtherChildrenId
            };
        }
    }
}
