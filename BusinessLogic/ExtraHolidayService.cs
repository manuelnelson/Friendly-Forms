using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class ExtraHolidayService : FormService<IExtraHolidayRepository, ExtraHoliday>, IExtraHolidayService
    {
        public ExtraHolidayService(IExtraHolidayRepository formRepository)
            : base(formRepository)
        {
        }

        public List<ExtraHoliday> GetByChildId(long childId)
        {
            try
            {
                var enumerable = FormRepository.GetFiltered(e=>e.ChildId == childId);
                return enumerable == null ? new List<ExtraHoliday>() : enumerable.ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
  }
}
