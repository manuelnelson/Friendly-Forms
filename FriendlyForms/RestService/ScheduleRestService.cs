using System;
using System.Runtime.Serialization;
using BusinessLogic.Contracts;
using Models;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace FriendlyForms.RestService
{
    [DataContract]
    [Route("/Schedule/")]
    public class ReqSchedule
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public int DetermineBeginDate { get; set; }
        [DataMember]
        public string BeginDate { get; set; }
        [DataMember]
        public string CustodianWeekendOther { get; set; }
        [DataMember]
        public string NonCustodianWeekendOther { get; set; }
        [DataMember]
        public string WeekendDayStart { get; set; }
        [DataMember]
        public string WeekendDayEnd { get; set; }
        [DataMember]
        public string PickedUp { get; set; }
        [DataMember]
        public string PickupLocation { get; set; }
        [DataMember]
        public string DroppedOff { get; set; }
        [DataMember]
        public string DropOffLocation { get; set; }
        [DataMember]
        public int CustodianWeekend { get; set; }
        [DataMember]
        public int NonCustodianWeekend { get; set; }
        [DataMember]
        public int Weekdays { get; set; }
        [DataMember]
        public string WeekdayPickup { get; set; }
        [DataMember]
        public string WeekdayPickupLocation { get; set; }
        [DataMember]
        public string WeekdayDropoff { get; set; }
        [DataMember]
        public string WeekdayDropoffLocation { get; set; }
        [DataMember]
        public bool MondayParent { get; set; }
        [DataMember]
        public bool TuesdayParent { get; set; }
        [DataMember]
        public bool WednesdayParent { get; set; }
        [DataMember]
        public bool ThursdayParent { get; set; }
        [DataMember]
        public bool FridayParent { get; set; }
        [DataMember]
        public bool SaturdayParent { get; set; }
        [DataMember]
        public bool SundayParent { get; set; }
        [DataMember]
        public string AdditionalProvisions { get; set; }
    }

    [DataContract]
    public class RespSchedule : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
    [Authenticate]
    public class ScheduleRestService : ServiceBase
    {
        public IScheduleService ScheduleService { get; set; }

        public object Post(ReqSchedule request)
        {
            var scheduleViewModel = request.TranslateTo<ScheduleViewModel>();
            scheduleViewModel.UserId = Convert.ToInt32(UserSession.CustomId);
            ScheduleService.AddOrUpdate(scheduleViewModel);
            return new RespSchedule();
        }
        public object Put(ReqSchedule request)
        {
            var schedule = request.TranslateTo<Schedule>();
            schedule.UserId = Convert.ToInt32(UserSession.CustomId);
            ScheduleService.Update(schedule);
            return new RespSchedule();
        }
    }
}