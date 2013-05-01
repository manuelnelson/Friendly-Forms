using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class ExtraExpenseForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int HasExtraExpenses { get; set; }
        public virtual User User { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraExpenseFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
