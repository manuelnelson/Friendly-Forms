using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class VehicleViewModel : IViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [StringLength(100)]
        public string Make { get; set; }
        [Required]
        [StringLength(100)]
        public string VehicleModel { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Year must be a number")]
        public string Year { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [StringLength(100)]
        public string Owner { get; set; }

        public List<Vehicle> VehicleList { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Vehicle()
                {
                    Id = Id,
                    Make = Make,
                    Model = VehicleModel,
                    Owner = Owner,
                    UserId = UserId,
                    Year = Convert.ToInt16(Year)
                };
        }
    }
}
