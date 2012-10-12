using System.Collections.Generic;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Models
{
    public class AllHolidaysViewModel
    {
        public HolidayViewModel HolidayViewModel { get; set; }
        public ExtraHolidayViewModel ExtraHolidayViewModel { get; set; }
        public List<Holiday> ChildHolidays { get; set; }
        public List<ExtraHoliday> ExtraChildHolidays { get; set; } 
    }
}
