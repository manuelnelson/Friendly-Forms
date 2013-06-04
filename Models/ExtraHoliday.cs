using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ExtraHolidays")]
    public class ExtraHoliday : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string HolidayName { get; set; }
        public int HolidayFather { get; set; }
        public int HolidayMother { get; set; }

        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public long ChildId { get; set; }
        [Ignore]
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
