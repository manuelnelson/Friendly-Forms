using BusinessLogic.Contracts;
using DataInterface;
using Models;

namespace BusinessLogic
{
    public class ExtraExpenseFormService : FormService<IExtraExpenseFormRepository, ExtraExpenseForm>, IExtraExpenseFormService
    {
        private IExtraExpenseFormRepository ExtraExpenseFormRepository { get; set; }

        public ExtraExpenseFormService(IExtraExpenseFormRepository repository) : base(repository)
        {
            ExtraExpenseFormRepository = repository;
        }
    }
}
