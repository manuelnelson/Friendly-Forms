﻿using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IHealthService : IFormService<IHealthRepository, Health>
    {
    }
}
