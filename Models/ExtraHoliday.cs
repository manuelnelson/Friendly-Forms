using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class ExtraHoliday : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public string HolidayName { get; set; }
        public int HolidayFather { get; set; }
        public int HolidayMother { get; set; }

        public IViewModel ConvertToModel()
        {
            return new ExtraHolidayViewModel()
                {
                    ChildId = ChildId,
                    HolidayFather = HolidayFather,
                    HolidayMother = HolidayMother,
                    HolidayName = HolidayName,
                    UserId = UserId
                };
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
