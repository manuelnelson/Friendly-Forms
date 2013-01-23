﻿using System.Collections.Generic;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IPreexistingSupportService : IService<IPreexistingSupportRepository,PreexistingSupport>
    {
        List<PreexistingSupport> GetByUserId(int userId, bool isOtherParent = false);
        PreexistingSupport AddOrUpdate(PreexistingSupportViewModel model);
    }
}
