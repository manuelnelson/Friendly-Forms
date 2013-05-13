using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class ExtraHoliday : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public string HolidayName { get; set; }
        public int HolidayFather { get; set; }
        public int HolidayMother { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        public virtual Child Child { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraHolidayViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = this.TranslateTo<ExtraHoliday>();
            HolidayFather = updatingEntity.HolidayFather;
            HolidayMother = updatingEntity.HolidayMother;
            HolidayName = updatingEntity.HolidayName;
        }
    }
}
