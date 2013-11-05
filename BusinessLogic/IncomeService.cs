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
                return FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public bool HasNonW2Income(long userId)
        {
            try
            {
                var firstOrDefault = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent).FirstOrDefault();
                return firstOrDefault != null && (FormRepository.GetByUserId(userId).HasNonW2Income() || firstOrDefault.HasNonW2Income());
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Could not determine Non W2 Income", ex);
            }
        }
        public bool HasSelfIncome(long userId)
        {
            try
            {
                var otherParent = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent).FirstOrDefault();
                var primaryParent = FormRepository.GetByUserId(userId);
                return ((otherParent != null && otherParent.SelfIncome > 0) || (primaryParent != null &&
                       primaryParent.SelfIncome > 0));
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Could not determine Non W2 Income", ex);
            }
        }
    }
}
