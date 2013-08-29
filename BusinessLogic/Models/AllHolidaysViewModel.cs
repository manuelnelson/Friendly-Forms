using System.Collections.Generic;
using System.Runtime.Serialization;
using Models;

namespace BusinessLogic.Models
{
    [DataContract]
    public class AllHolidaysViewModel
    {
        //[DataMember]
        //public HolidayViewModel HolidayViewModel { get; set; }
        //[DataMember]
        //public ExtraHolidayViewModel ExtraHolidayViewModel { get; set; }
        [DataMember]
        public List<Holiday> ChildHolidays { get; set; }
        [DataMember]
        public List<ExtraHoliday> ExtraChildHolidays { get; set; } 
    }
}
