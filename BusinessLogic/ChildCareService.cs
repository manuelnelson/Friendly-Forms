using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ChildCareService : FormService<IChildCareRepository, ChildCare, ChildCareViewModel>, IChildCareService
    {
        private IChildCareRepository ChildCareRepository { get; set; }

        public ChildCareService(IChildCareRepository repository) : base(repository)
        {
            ChildCareRepository = repository;
        }

        public ChildCare GetByChildId(long childId)
        {
            return ChildCareRepository.GetChildById(childId);
        }

        public List<ChildCare> GetAllByUserId(long userId)
        {
            return ChildCareRepository.GetAllByUserId(userId).ToList();
        }
    }
}
