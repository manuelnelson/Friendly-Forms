using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class CommunicationHelper
    {
        public static CommunicationViewModel ConvertToModel(this Communication communication)
        {
            return new CommunicationViewModel()
                {
                    AllowCommunication = communication.AllowCommunication,
                    Email = communication.Email,
                    FatherCommunicate = communication.FatherCommunicate,
                    Limitations = communication.Limitations,
                    MotherCommunicate = communication.MotherCommunicate,
                    Other = communication.Other,
                    OtherMethod = communication.OtherMethod,
                    Telephone = communication.Telephone,
                    UserId = communication.UserId
                };
        }
    }
}
