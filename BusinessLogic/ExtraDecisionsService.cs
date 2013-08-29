using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class ExtraDecisionsService : FormService<IExtraDecisionRepository, ExtraDecisions>, IExtraDecisionsService
    {
        public ExtraDecisionsService(IExtraDecisionRepository formRepository) : base(formRepository){}

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
