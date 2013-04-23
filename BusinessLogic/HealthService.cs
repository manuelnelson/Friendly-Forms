using System;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class HealthService : FormService<IHealthRepository, Health, HealthViewModel>, IHealthService
    {
        private IHealthRepository HealthRepository { get; set; }

        public HealthService(IHealthRepository repository) : base(repository)
        {
            HealthRepository = repository;
        }

        public HealthViewModel GetByUserId(int userId, bool isOtherParent = false)
        {
            try
            {
                var entity = FormRepository.GetFiltered(m => m.UserId == userId && m.IsOtherParent == isOtherParent).FirstOrDefault();
                return (entity == null ? new HealthViewModel() : entity.ConvertToModel()) as HealthViewModel;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }

        }
    }
}
