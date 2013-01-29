using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataLayerContext;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ExtraHolidayService : FormService<ExtraHolidayRepository,ExtraHoliday,ExtraHolidayViewModel>, IExtraHolidayService
    {
        public ExtraHolidayService(ExtraHolidayRepository formRepository) : base(formRepository)
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

        public ExtraHoliday AddOrUpdate(ExtraHolidayViewModel model)
        {
            try
            {
                var entity = model.ConvertToEntity();
                //Check if court already exists and we need to update record
                var existCourt = FormRepository.Get(model.Id);
                if (existCourt != null)
                {
                    existCourt.Update(entity);
                    FormRepository.Update(existCourt);
                    return existCourt;
                }
                FormRepository.Add((ExtraHoliday) entity);
                return (ExtraHoliday) entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

  }
}
