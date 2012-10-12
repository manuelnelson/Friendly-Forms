using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Vehicle : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Owner { get; set; }

        public IViewModel ConvertToModel()
        {
            return new VehicleViewModel()
                {
                    Make = Make,
                    VehicleModel = Model,
                    Owner = Owner,
                    UserId = UserId,
                    Year = Year.ToString()
                };
        }
        
        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Vehicle) entity;
            UserId = updatingEntity.UserId;
            Make = updatingEntity.Make;
            Model = updatingEntity.Model;
            Year = updatingEntity.Year;
            Owner = updatingEntity.Owner;
        }
    }
}
