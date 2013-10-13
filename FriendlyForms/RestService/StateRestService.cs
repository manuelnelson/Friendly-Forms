using BusinessLogic.Contracts;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace FriendlyForms.RestService
{
    public class StateRestService
    {
        [Route("/States", "GET")]
        [Route("/States")]
        [Route("/States/{Id}")]
        public class StateDto : IReturn<StateDto>
        {
            public long[] Ids { get; set; }
            public long Id { get; set; }
            public string Abbreviation { get; set; }
            public string Name { get; set; }
        }

        public class StatesService : Service
        {
            public IStateService StateService { get; set; } //Injected by IOC

            public object Get(StateDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return StateService.Get(request.Ids);
                if (request.Id > 0)
                    return StateService.Get(request.Id);
                return StateService.GetAll();
            }

        }

    }

}
