using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class OtherChildrenService : FormService<IOtherChildrenRepository, OtherChildren>, IOtherChildrenService
    {
        private IOtherChildRepository OtherChildRepository { get; set; }
        public OtherChildrenService(IOtherChildrenRepository formRepository, IOtherChildRepository otherChildRepository)
            : base(formRepository)
        {
            OtherChildRepository = otherChildRepository;
        }

        public OtherChildren GetByUserId(long userId, bool isOtherParent = false)
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

        public bool HasOtherChildren(long userId)
        {
            try
            {
                var primaryForm = FormRepository.GetByUserId(userId);
                var children = OtherChildRepository.GetFiltered(c => c.OtherChildrenId == primaryForm.Id);
                var otherForm = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent).FirstOrDefault();
                var otherChildren = OtherChildRepository.GetFiltered(c => c.OtherChildrenId == otherForm.Id);
                return children.Any() || otherChildren.Any();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to determine other children", ex);
            }
        }
    }
}
