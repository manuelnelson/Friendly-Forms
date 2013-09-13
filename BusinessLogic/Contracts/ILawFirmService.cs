﻿using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface ILawFirmService : IService<ILawFirmRepository, LawFirm>
    {
    }

}