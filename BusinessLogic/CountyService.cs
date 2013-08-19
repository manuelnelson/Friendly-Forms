using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class CountyService : Service<ICountyRepository, County>, ICountyService
    {
        private readonly ICountyRepository _countyRepository;

        public CountyService(ICountyRepository countyRepository)
            : base(countyRepository)
        {
            _countyRepository = countyRepository;
        }
        public IEnumerable<County> GetAll()
        {
            try
            {
                return _countyRepository.GetAll();
            }
            catch(Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve county information", ex);
            }
        }
    }
}
