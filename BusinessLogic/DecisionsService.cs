using System;
using BusinessLogic.Contracts;
using DataLayerContext.Repositories;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DecisionsService : FormService<DecisionRepository, Decisions, DecisionsViewModel>, IDecisionsService
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

        public DecisionsViewModel GetByChildId(int childId)
        {
            try
            {
                var decisions = FormRepository.GetByChildId(childId);
                return decisions == null ? new DecisionsViewModel() : decisions.ConvertToModel() as DecisionsViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve child information", ex);
            }
        }
    }
}
