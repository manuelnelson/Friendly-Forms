using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class PreexistingSupportChildService : FormService<IPreexistingSupportChildRepository, PreexistingSupportChild>, IPreexistingSupportChildService
    {
        public PreexistingSupportChildService(IPreexistingSupportChildRepository formRepository)
            : base(formRepository)
        {
        }

        public IEnumerable<PreexistingSupportChild> GetChildrenBySupportId(long preexistingSupportId)
        {
            try
            {
                return FormRepository.GetChildrenById(preexistingSupportId);
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

    }
}
