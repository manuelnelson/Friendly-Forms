using System;
using System.Collections.Generic;
using BusinessLogic.Contracts;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class LawFirmRestService
    {
        //REST Resource DTO
        [Route("/LawFirms")]
        [Route("/LawFirms/{Ids}")]
        public class LawFirmListDto : IReturn<List<LawFirmDto>>
        {
            public long[] Ids { get; set; }

            public LawFirmListDto(params long[] ids)
            {
                Ids = ids;
            }
        }

        [Route("/LawFirms", "POST")]
        [Route("/LawFirms/", "PUT")]
        [Route("/LawFirms/{Id}", "GET")]
        [Route("/LawFirms", "GET")]
        public class LawFirmDto : IReturn<LawFirmDtoResp>
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string CityState { get; set; }
            public string Zip { get; set; }
            public int Subscription { get; set; }
        }

        public class LawFirmsService : Service
        {
            public ILawFirmService LawFirmService { get; set; } //Injected by IOC

            public object Get(LawFirmDto request)
            {
                return LawFirmService.Get(request.Id);
            }


            public object Post(LawFirmDto request)
            {
                var lawFirmEntity = request.TranslateTo<LawFirm>();
                if (request.CityState.Contains(","))
                {
                    var cityState = request.CityState.Split(',');
                    lawFirmEntity.City = cityState[0];
                    lawFirmEntity.State = cityState[1];
                    LawFirmService.Add(lawFirmEntity);
                    return lawFirmEntity;                    
                }
                throw new ArgumentException("Not a valid city state. Fields must be separated by a comma");
            }

            public object Put(LawFirmDto request)
            {
                var lawFirmEntity = request.TranslateTo<LawFirm>();
                LawFirmService.Update(lawFirmEntity);
                return lawFirmEntity;
            }

            public void Delete(LawFirmDto request)
            {
                var lawFirmEntity = request.TranslateTo<LawFirm>();
                LawFirmService.Delete(lawFirmEntity);
            }
        }

    }

    public class LawFirmDtoResp
    {
        public List<LawFirm> LawFirms { get; set; }
    }
}
