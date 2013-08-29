using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class HolidayService : FormService<IHolidayRepository, Holiday>,IHolidayService
    {
        public HolidayService(IHolidayRepository formRepository): base(formRepository)
        {
        }

        /// <summary>
        /// Retreives holiday infromation by childId
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public Holiday GetByChildId(long childId)
        {
            try
            {
                return FormRepository.GetByChildId(childId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve holiday information", ex);
            }
        }

        public List<Holiday> GetChildrenListByUserId(long userId)
        {
            try
            {
                return FormRepository.GetChildListByUserId(userId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve child information", ex);
            }
        }
    }
}
