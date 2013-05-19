using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class Communication : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
