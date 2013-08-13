using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ExtraDecisionsService : FormService<IExtraDecisionRepository, ExtraDecisions>, IExtraDecisionsService
    {
        public ExtraDecisionsService(IExtraDecisionRepository formRepository) : base(formRepository){}
        public ExtraDecisions AddOrUpdate(ExtraDecisionsViewModel model)
        {
            try
            {
                var entity = model.ConvertToEntity();
                //Check if court already exists and we need to update record
                var existCourt = FormRepository.Get(model.Id);
                if (existCourt != null)
                {
                    existCourt.DecisionMaker = entity.DecisionMaker;
                    existCourt.Description = entity.Description;
                    existCourt.ChildId = entity.ChildId;
                    FormRepository.Update(existCourt);
                    return existCourt;
                }
                FormRepository.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add decisions information", ex);
            }
        }

        public List<ExtraDecisions> GetByChildId(long childId)
        {
            try
            {
                var enumerable = FormRepository.GetFiltered(e => e.ChildId == childId);
                return enumerable == null ? new List<ExtraDecisions>() : enumerable.ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve decision information", ex);
            }            
        }
    }
}
