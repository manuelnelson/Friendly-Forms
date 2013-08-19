using DataInterface;
using Models;
using ServiceStack.OrmLite;

namespace DataLayerContext.OrmLiteRepositories
{
    public class BcsoOrmLiteRepository : OrmLiteRepository<Bcso>, IBcsoRepository
    {
        public BcsoOrmLiteRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public double GetAmount(int income, int numberOfChildren)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                switch (numberOfChildren)
                {
                    case 0:
                        return 0;
                    case 1:
                        return db.First<Bcso>(x=>x.GrossIncome == income).OneChildAmount;
                    case 2:
                        return db.First<Bcso>(x => x.GrossIncome == income).TwoChildAmount;
                    case 3:
                        return db.First<Bcso>(x => x.GrossIncome == income).ThreeChildAmount;
                    case 4:
                        return db.First<Bcso>(x => x.GrossIncome == income).FourChildAmount;
                    case 5:
                        return db.First<Bcso>(x => x.GrossIncome == income).FiveChildAmount;
                    default:
                        return db.First<Bcso>(x => x.GrossIncome == income).SixChildAmount;

                }

            }
        }
    }
}
