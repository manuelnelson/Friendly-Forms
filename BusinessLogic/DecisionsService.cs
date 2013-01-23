using System;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DecisionsService : Service<DecisionRepository, Decisions, DecisionsViewModel>, IDecisionsService
    {
        public DecisionsService(DecisionRepository formRepository) : base(formRepository)
        {
        }

        public void AddOrUpdate(DecisionsViewModel model)
        {
            try
            {
                var entity = model.ConvertToEntity() as Decisions;
                //Check if court already exists and we need to update record
                var existDecisions = FormRepository.GetByChildId(model.ChildId);
                if (existDecisions != null)
                {
                    existDecisions.Education = entity.Education;
                    existDecisions.HealthCare = entity.HealthCare;
                    existDecisions.Religion = entity.Religion;
                    existDecisions.ExtraCurricular = entity.ExtraCurricular;
                    existDecisions.BothResolve = entity.BothResolve;
                    FormRepository.Update(existDecisions);
                    return;
                }
                FormRepository.Add(entity);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to add decisions information", ex);
            }
        }

        public Decisions GetByChildId(long childId)
        {
            try
            {
                return FormRepository.GetByChildId(childId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve child information", ex);
            }
        }
    }
}
