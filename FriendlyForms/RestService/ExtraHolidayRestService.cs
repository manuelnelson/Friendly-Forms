using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models.ViewModels;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/ExtraHoliday/", Verbs = "POST")]
    [Route("/ExtraHoliday/{ChildId}", Verbs = "GET")]
    public class ReqExtraHoliday
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int ChildId { get; set; }
        [DataMember]
        public string HolidayName { get; set; }
        [DataMember]
        public int HolidayFather { get; set; }
        [DataMember]
        public int HolidayMother { get; set; }
    }

    [DataContract]
    public class RespExtraHoliday : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ExtraHolidayRestService : Service
    {
        public IExtraHolidayService ExtraHolidayService { get; set; }
        public object Get(ReqExtraHoliday request)
        {
            var extraHolidays = ExtraHolidayService.GetByChildId(request.ChildId).ExtraHolidays;
            return extraHolidays;
        }

        public object Post(ReqExtraHoliday request)
        {
            var extraHolidayViewModel = request.TranslateTo<ExtraHolidayViewModel>();
            ExtraHolidayService.AddOrUpdate(extraHolidayViewModel);
            return new RespExtraHoliday();
        }
    }
}