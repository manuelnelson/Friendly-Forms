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
                    LimitationDetails = communication.LimitationDetails,
                    Limitations = communication.Limitations,
                    Other = communication.Other,
                    OtherMethod = communication.OtherMethod,
                    Telephone = communication.Telephone,
                    UserId = communication.UserId
                };
        }
    }
}
