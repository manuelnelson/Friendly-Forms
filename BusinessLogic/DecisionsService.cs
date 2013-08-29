using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class DecisionsService : FormService<IDecisionRepository, Decisions>, IDecisionsService
    {
        public DecisionsService(IDecisionRepository formRepository)
            : base(formRepository)
        {
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

        public List<Decisions> GetChildrenListByUserId(long userId)
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
