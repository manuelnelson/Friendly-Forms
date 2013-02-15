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
    [Route("/Schedule/")]
    public class ReqSchedule
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int DetermineBeginDate { get; set; }
        [DataMember]
        public string BeginDate { get; set; }
        [DataMember]
        public string FatherWeekendOther { get; set; }
        [DataMember]
        public string MotherWeekendOther { get; set; }
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
        public int FatherWeekend { get; set; }
        [DataMember]
        public int MotherWeekend { get; set; }
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
        public int? MondayParent { get; set; }
        [DataMember]
        public int? TuesdayParent { get; set; }
        [DataMember]
        public int? WednesdayParent { get; set; }
        [DataMember]
        public int? ThursdayParent { get; set; }
        [DataMember]
        public int? FridayParent { get; set; }
        [DataMember]
        public string AdditionalProvisions { get; set; }
    }

    [DataContract]
    public class RespSchedule : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ScheduleRestService : Service
    {
        public IScheduleService ScheduleService { get; set; }

        public object Post(ReqSchedule request)
        {
            var scheduleViewModel = request.TranslateTo<ScheduleViewModel>();
            ScheduleService.AddOrUpdate(scheduleViewModel);
            return new RespSchedule();
        }
        public object Put(ReqSchedule request)
        {
            var schedule = request.TranslateTo<Schedule>();
            ScheduleService.Update(schedule);
            return new RespSchedule();
        }
    }
}