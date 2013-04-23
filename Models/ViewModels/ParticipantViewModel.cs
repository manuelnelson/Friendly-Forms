using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class ParticipantViewModel : IViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [Required]
        [Display(Name = "Plaintiff's Name")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string PlaintiffsName { get; set; }

        [Required]
        [Display(Name = "Plaintiff Relationship")]
        public int PlaintiffRelationship { get; set; }

        [Required]
        [Display(Name = "Plaintiff Custodial Parent")]
        public int PlaintiffCustodialParent { get; set; }

        [Required]
        [Display(Name = "Defendant's Name")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string DefendantsName { get; set; }

        [Required]
        [Display(Name = "Defendant's Relationship")]
        public int DefendantRelationship { get; set; }

        [Required]
        [Display(Name = "Defendant Custodial Parent")]
        public int DefendantCustodialParent { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Participant>();
        } 
    }



}