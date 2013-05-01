using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ExtraExpenseFormService : FormService<IExtraExpenseFormRepository, ExtraExpenseForm, ExtraExpenseFormViewModel>, IExtraExpenseFormService
    {
        private IExtraExpenseFormRepository ExtraExpenseFormRepository { get; set; }

        public ExtraExpenseFormService(IExtraExpenseFormRepository repository) : base(repository)
        {
            ExtraExpenseFormRepository = repository;
        }
    }
}
