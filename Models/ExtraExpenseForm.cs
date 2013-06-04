using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ExtraExpenseForms")]
    public class ExtraExpenseForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        public int HasExtraExpenses { get; set; }
        [Ignore]
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
