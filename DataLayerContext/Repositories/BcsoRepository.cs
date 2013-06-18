using System.Linq;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class BcsoRepository : Repository<Bcso>, IBcsoRepository
    {
        public BcsoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double GetAmount(int income, int numberOfChildren)
        {
            switch (numberOfChildren)
            {
                case 0:
                    return 0;
                case 1:
                    return GetDbSet().First(x => x.GrossIncome == income).OneChildAmount;
                case 2:
                    return GetDbSet().First(x => x.GrossIncome == income).TwoChildAmount;
                case 3:
                    return GetDbSet().First(x => x.GrossIncome == income).ThreeChildAmount;
                case 4:
                    return GetDbSet().First(x => x.GrossIncome == income).FourChildAmount;
                case 5:
                    return GetDbSet().First(x => x.GrossIncome == income).FiveChildAmount;
               default:
                    return GetDbSet().First(x => x.GrossIncome == income).SixChildAmount;

            }
        }
    }
}
