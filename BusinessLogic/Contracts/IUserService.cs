using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IUserService
    {
        void Add();
        bool DoesNativeUserExist(string email);
        AccountUser CreateNativeUser(string email, string firstName, string lastName, string password);
        AccountUser Authenticate(string email, string password);
    }
}
