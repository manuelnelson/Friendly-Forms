﻿using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IIncomeService : IService<IIncomeRepository, Income>
    {
        IncomeViewModel GetByUserId(int userId, bool isOtherParent = false);
        Income AddOrUpdate(IncomeViewModel model);

    }
}
