using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class PreexistingSupportFormService : FormService<IPreexistingSupportFormRepository, PreexistingSupportForm>, IPreexistingSupportFormService
    {
        private IPreexistingSupportFormRepository PreexistingSupportFormRepository { get; set; }

        public PreexistingSupportFormService(IPreexistingSupportFormRepository repository) : base(repository)
        {
            PreexistingSupportFormRepository = repository;
        }

        public PreexistingSupportForm GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
