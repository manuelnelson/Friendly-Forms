using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class PreexistingSupportFormService : FormService<IPreexistingSupportFormRepository, PreexistingSupportForm>, IPreexistingSupportFormService
    {
        private IPreexistingSupportFormRepository PreexistingSupportFormRepository { get; set; }

        public PreexistingSupportFormService(IPreexistingSupportFormRepository repository) : base(repository)
        {
            PreexistingSupportFormRepository = repository;
        }

        public PreexistingSupportFormViewModel GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
                return (entity == null ? new PreexistingSupportFormViewModel() : entity.ConvertToModel()) as PreexistingSupportFormViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
