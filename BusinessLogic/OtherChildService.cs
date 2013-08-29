using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class OtherChildService : FormService<IOtherChildRepository, OtherChild>, IOtherChildService
    {
        public OtherChildService(IOtherChildRepository formRepository)
            : base(formRepository)
        {
        }


        public IEnumerable<OtherChild> GetChildrenByOtherChildrenId(long otherChildrenId)
        {
            try
            {
                return FormRepository.GetFiltered(c => c.OtherChildrenId == otherChildrenId);
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve children information", ex);
            }
        }
    }
}
