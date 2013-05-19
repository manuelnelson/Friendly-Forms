using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class ExtraHoliday : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public string HolidayName { get; set; }
        public int HolidayFather { get; set; }
        public int HolidayMother { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraHolidayViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (ExtraHoliday) entity;
            HolidayFather = updatingEntity.HolidayFather;
            HolidayMother = updatingEntity.HolidayMother;
            HolidayName = updatingEntity.HolidayName;
        }
    }
}
