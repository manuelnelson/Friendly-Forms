using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Vehicle : IEntity, IFormEntity
    {
        public long Id { get; set; }        
        public long UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Owner { get; set; }
        public int Refinanced { get; set; }
        public string Name { get; set; }
        public DateTime? RefinanceDate { get; set; }

        public int VehicleFormId { get; set; }
        public virtual VehicleForm VehicleForm { get; set; }

        public IViewModel ConvertToModel()
        {
            return new VehicleViewModel()
                {
                    Make = Make,
                    VehicleModel = Model,
                    Owner = Owner,
                    UserId = UserId,
                    RefinanceDate = RefinanceDate.HasValue ? RefinanceDate.Value.ToString("MM/dd/yyyy") : "Not Provided",
                    Name = Name,
                    Refinanced = Refinanced,
                    Year = Year.ToString(),
                    VehicleFormId = VehicleFormId
                };
        }
        
        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Vehicle) entity;
            UserId = updatingEntity.UserId;
            Make = updatingEntity.Make;
            Model = updatingEntity.Model;
            Year = updatingEntity.Year;
            Refinanced = updatingEntity.Refinanced;
            Name = updatingEntity.Name;
            RefinanceDate = updatingEntity.RefinanceDate;
            Owner = updatingEntity.Owner;
            VehicleFormId = updatingEntity.VehicleFormId;
        }
    }
}
