using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IHealthService : IFormService<IHealthRepository, Health>
    {
        /// <summary>
        /// Returns TViewModel infromation by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        HealthViewModel GetByUserId(int userId, bool isOtherParent = false);
    }
}
