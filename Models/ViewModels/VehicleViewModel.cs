using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Models.Contract;

namespace Models.ViewModels
{
    public class VehicleViewModel : IViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string Make { get; set; }
        [Required]
        [StringLength(100)]
        public string VehicleModel { get; set; }
        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Year must be a number")]
        public string Year { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string Owner { get; set; }

        [Required]
        public int Refinanced { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        public string RefinanceDate { get; set; }

        public int VehicleFormId { get; set; }

        public List<Vehicle> VehicleList { get; set; }
        public List<SelectListItem> Names { get; set; }
 
        public IFormEntity ConvertToEntity()
        {
            return new Vehicle()
                {
                    Id = Id,
                    Make = Make,
                    Model = VehicleModel,
                    Owner = Owner,
                    UserId = UserId,
                    Refinanced = Refinanced,
                    Name = Name,
                    RefinanceDate = string.IsNullOrEmpty(RefinanceDate) ? (DateTime?) null : Convert.ToDateTime(RefinanceDate),
                    Year = Convert.ToInt16(Year),
                    VehicleFormId = VehicleFormId
                };
        }
    }
}
