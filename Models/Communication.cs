using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Communications")]
    public class Communication : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int AllowCommunication { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool Other { get; set; }
        public string OtherMethod { get; set; }
        public int Limitations { get; set; }
        public string LimitationDetails { get; set; }
        public int Notification { get; set; }
        public int AccessOfRights { get; set; }
        public string AccessOfRightsDetails { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<CommunicationViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (Communication)entity;
            AllowCommunication = update.AllowCommunication;
            Email = update.Email;
            LimitationDetails = update.LimitationDetails;
            Limitations = update.Limitations;
            Other = update.Other;
            OtherMethod = update.OtherMethod;
            Telephone = update.Telephone;
            UserId = update.UserId;
            Notification = update.Notification;
            AccessOfRights = update.AccessOfRights;
            AccessOfRightsDetails = update.AccessOfRightsDetails;
        }


    }
}
