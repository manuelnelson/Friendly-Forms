using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterface;
using Models;

namespace BusinessLogic.Contracts
{
    public interface IDebtService : IFormService<IDebtRepository, Debt>
    {
    }
}
