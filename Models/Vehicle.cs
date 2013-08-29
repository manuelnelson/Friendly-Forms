using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Vehicles")]
    public class Vehicle : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }        
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Owner { get; set; }
        public int Refinanced { get; set; }
        public string Name { get; set; }
        public DateTime? RefinanceDate { get; set; }

        public int VehicleFormId { get; set; }
        [Ignore]
        public virtual VehicleForm VehicleForm { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
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
