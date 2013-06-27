using Models;

namespace DataInterface
{
    public interface IDeviationsRepository : IFormRepository<Deviations>
    {
        Deviations GetChildById(long childId);
    }
}
