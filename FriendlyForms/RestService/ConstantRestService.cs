using BusinessLogic.Contracts;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class ConstantRestService
    {
        [Route("/Constants", "POST")]
        [Route("/Constants", "PUT")]
        [Route("/Constants", "GET")]
        [Route("/Constants", "DELETE")]
        [Route("/Constants")]
        [Route("/Constants/{Id}")]
        public class ConstantDto : IReturn<ConstantDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
        }

        public class ConstantsService : Service
        {
            public IConstantsService ConstantService { get; set; } //Injected by IOC

            public object Get(ConstantDto request)
            {
                return ConstantService.GetConstants();
            }
        }

    }

}
