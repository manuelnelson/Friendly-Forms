using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

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

        /// <summary>
        /// Add the holiday information to the database.
        /// </summary>
        /// <param name="holiday"></param>
        public Holiday AddOrUpdate(HolidayViewModel holiday)
        {
            try
            {
                var entity = holiday.ConvertToEntity();
                //Check if court already exists and we need to update record
                var existDecisions = FormRepository.GetByChildId(holiday.ChildId);
                if (existDecisions != null)
                {
                    existDecisions.Update(entity);
                    FormRepository.Update(existDecisions);
                    return existDecisions;
                }
                FormRepository.Add((Holiday) entity);
                return (Holiday) entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add holiday information", ex);
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
