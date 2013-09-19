using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using FriendlyForms.Helpers;
using Models;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/ExtraHolidays/", Verbs = "POST")]
    [Route("/ExtraHolidays/")]
    public class ReqExtraHoliday : IHasUser
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public long ChildId { get; set; }
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
        public List<ExtraHoliday> ExtraHolidays { get; set; }
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class RespExtraHolidayPost : IHasResponseStatus
    {
        [DataMember]
        public ExtraHoliday ExtraHoliday { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Authenticate]
    public class ExtraHolidayRestService : ServiceBase
    {
        public IExtraHolidayService ExtraHolidayService { get; set; }
        public object Get(ReqExtraHoliday request)
        {
            var extraHolidays = ExtraHolidayService.GetByChildId(request.ChildId);
            return new RespExtraHoliday()
                {
                    ExtraHolidays = extraHolidays
                };
        }

        public object Post(ReqExtraHoliday request)
        {
            var extraHoliday = request.TranslateTo<ExtraHoliday>();
            ExtraHolidayService.Add(extraHoliday);
            return extraHoliday;
        }
        public object Put(ReqExtraHoliday request)
        {
            var extraHoliday = request.TranslateTo<ExtraHoliday>();
            ExtraHolidayService.Update(extraHoliday);
            return new RespExtraHolidayPost();
        }
    }
}