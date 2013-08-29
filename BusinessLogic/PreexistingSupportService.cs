using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class PreexistingSupportService : FormService<IPreexistingSupportRepository, PreexistingSupport>, IPreexistingSupportService
    {
        public PreexistingSupportService(IPreexistingSupportRepository formRepository)
            : base(formRepository)
        {
        }

        public List<PreexistingSupport> GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent ==isOtherParent).ToList();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

    }
}
