using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class CountyRestService
    {
        //REST Resource DTO
        [Route("/Counties")]
        [Route("/Counties/{Ids}")]
        public class CountyListDto : IReturn<List<CountyDto>>
        {
            public long[] Ids { get; set; }

            public CountyListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/Counties", "POST")]
        [Route("/Counties/", "PUT")]
        [Route("/Counties")]
        public class CountyDto : IReturn<RespCountyDto>
        {
            public long Id { get; set; }
        }

        public class CountiesService : Service
        {
            public ICountyService Countieservice { get; set; } //Injected by IOC

            public object Get(CountyDto request)
            {
                if(request.Id != 0)
                    return Countieservice.Get(request.Id);
                return new RespCountyDto
                    {
                        Counties = Countieservice.GetAll().ToList()
                    };
            }

            public object Get(CountyListDto request)
            {
                //TODO Do something more interested.  Add query possibly 
                return Countieservice.GetFiltered(t => t.Id != 0);
            }

            public object Post(CountyDto request)
            {
                var CountyEntity = request.TranslateTo<County>();
                Countieservice.Add(CountyEntity);
                return CountyEntity;
            }

            public object Put(CountyDto request)
            {
                var CountyEntity = request.TranslateTo<County>();
                Countieservice.Update(CountyEntity);
                return CountyEntity;
            }

            public void Delete(CountyListDto request)
            {
                Countieservice.DeleteAll(request.Ids);
            }

            public void Delete(CountyDto request)
            {
                var CountyEntity = request.TranslateTo<County>();
                Countieservice.Delete(CountyEntity);
            }
        }

    }

    public class RespCountyDto
    {
        public County County { get; set; }
        public List<County> Counties { get; set; } 
    }
}
