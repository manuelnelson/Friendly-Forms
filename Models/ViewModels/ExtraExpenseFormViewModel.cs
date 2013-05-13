﻿using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class ExtraExpenseFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public int HasExtraExpenses { get; set; }

        public long UserId { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<ExtraExpenseForm>();
        }

    }
}
