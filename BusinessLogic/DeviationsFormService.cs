using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class DeviationsFormService : FormService<IDeviationsFormRepository, DeviationsForm, DeviationsFormViewModel>, IDeviationsFormService
    {
        private IDeviationsFormRepository DeviationsFormRepository { get; set; }

        public DeviationsFormService(IDeviationsFormRepository repository) : base(repository)
        {
            DeviationsFormRepository = repository;
        }

        public DeviationsFormViewModel GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
                return (entity == null ? new DeviationsFormViewModel() : entity.ConvertToModel()) as DeviationsFormViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
