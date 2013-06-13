using Models;

namespace DataInterface
{
    public interface IBcsoRepository : IRepository<Bcso>
    {
        double GetAmount(double income, int numberOfChildren);
    }
}