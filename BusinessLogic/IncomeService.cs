using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class IncomeService : FormService<IIncomeRepository, Income>, IIncomeService
    {
        public IncomeService(IIncomeRepository formRepository)
            : base(formRepository)
        {
        }

        public Income GetByUserId(long userId, bool isOtherParent = false)
        {
            try
            {
                return FormRepository.GetFiltered(m=>m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

    }
}
