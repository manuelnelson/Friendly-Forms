using Models;

namespace DataInterface
{
    public interface IBcsoRepository : IRepository<Bcso>
    {
        double GetAmount(int income, int numberOfChildren);
    }
}