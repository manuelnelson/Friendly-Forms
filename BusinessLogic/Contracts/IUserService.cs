using BusinessLogic.Models;
using Models.ViewModels;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        bool DoesNativeUserExist(string email);
        AccountUser CreateNativeUser(string email, string firstName, string lastName, string password, int roleId = (int)Role.Default);
        AccountUser Authenticate(string email, string password);
    }
}
