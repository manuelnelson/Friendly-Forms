using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class OtherChildrenService : FormService<IOtherChildrenRepository, OtherChildren>, IOtherChildrenService
    {
        public OtherChildrenService(IOtherChildrenRepository formRepository)
            : base(formRepository)
        {
        }

        public OtherChildrenViewModel GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
                return (entity == null ? new OtherChildrenViewModel() : entity.ConvertToModel()) as OtherChildrenViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }

        }
    }
}
