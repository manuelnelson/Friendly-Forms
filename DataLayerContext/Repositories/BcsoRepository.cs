using System;
using DataInterface;
using Models;

namespace DataLayerContext.Repositories
{
    public class BcsoRepository : Repository<Bcso>, IBcsoRepository
    {
        public BcsoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double GetAmount(double income, int numberOfChildren)
        {
            throw new NotImplementedException();
        }
    }
}
