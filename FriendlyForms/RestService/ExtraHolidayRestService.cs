using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
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
            return extraHolidays;
        }

        public object Post(ReqExtraHoliday request)
        {
            var extraHolidayViewModel = request.TranslateTo<ExtraHolidayViewModel>();
            extraHolidayViewModel.UserId = Convert.ToInt64(UserSession.Id);
            var extraHoliday = ExtraHolidayService.AddOrUpdate(extraHolidayViewModel);
            return new RespExtraHolidayPost()
                {
                    ExtraHoliday = extraHoliday
                };
        }
        public object Put(ReqExtraHoliday request)
        {
            var extraHoliday = request.TranslateTo<ExtraHoliday>();
            extraHoliday.UserId = Convert.ToInt64(UserSession.Id);
            ExtraHolidayService.Update(extraHoliday);
            return new RespExtraHolidayPost();
        }
    }
}