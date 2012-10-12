using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Communication : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AllowCommunication { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool Other { get; set; }
        public string OtherMethod { get; set; }
        public int Limitations { get; set; }
        public string FatherCommunicate { get; set; }
        public string MotherCommunicate { get; set; }

        public IViewModel ConvertToModel()
        {
            return new CommunicationViewModel()
            {
                AllowCommunication = AllowCommunication,
                Email = Email,
                FatherCommunicate = FatherCommunicate,
                Limitations = Limitations,
                MotherCommunicate = MotherCommunicate,
                Other = Other,
                OtherMethod = OtherMethod,
                Telephone = Telephone,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var update = (Communication)entity;
            AllowCommunication = update.AllowCommunication;
            Email = update.Email;
            FatherCommunicate = update.FatherCommunicate;
            Limitations = update.Limitations;
            MotherCommunicate = update.MotherCommunicate;
            Other = update.Other;
            OtherMethod = update.OtherMethod;
            Telephone = update.Telephone;
            UserId = update.UserId;
        }


    }
}
