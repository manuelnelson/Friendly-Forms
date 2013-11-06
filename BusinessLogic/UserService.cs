using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;
using ServiceStack.ServiceInterface.Auth;

namespace BusinessLogic
{
    public class UserService : Service<IUserRepository, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public User CreateOrUpdate(User user)
        {
            try
            {
                //Check if user has an account already created
                var accountUser = _userRepository.GetByUserAuthId(user.UserAuthId);
                if (accountUser != null)
                {
                    accountUser.UserAuthId = user.UserAuthId;
                    _userRepository.Update(accountUser);
                    return accountUser;
                }

                //User has never logged in.  Create user
                _userRepository.Add(user);
                return user;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        public User GetByUserAuthId(int userAuthId)
        {
            try
            {
                return _userRepository.GetByUserAuthId(userAuthId);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }

        private const int MaximumDaysInMonth = 31;

        /// <summary>
        /// Gets all accounts that are a) active and b) same day as recurring start date
        /// </summary>
        /// <returns></returns>
        public List<User> GetTodaysActiveAccounts()
        {
            var currentDate = DateTime.UtcNow;
            var currentDay = currentDate.Day;
            //current day users
            var users = _userRepository.GetFiltered(x => x.RecurringActive).ToList().Where(x=>x.RecurringDateStart != null && x.RecurringDateStart.Value.Day == currentDay).ToList();
            var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            if (currentDay != daysInMonth || currentDay == MaximumDaysInMonth)
                return users;
            //else we need to retrieve later possible days in month as well
            var difference = MaximumDaysInMonth - currentDay;
            for (var i = 0; i < difference; i++)
            {
                var day = MaximumDaysInMonth - i;
                var laterMonthUsers = _userRepository.GetFiltered(x => x.RecurringActive && x.RecurringDateStart.Value.Day == day).ToList();
                users.AddRange(laterMonthUsers);
            }
            return users;
        }

        public int GetNumberOfUsersAddedThisMonth(User adminUser)
        {
            try
            {
                var monthAgo = DateTime.UtcNow.AddMonths(-1);
                //Get law firm attorneys
                var firmUsers = _userRepository.GetFiltered(x => x.LawFirmId == adminUser.LawFirmId);
                var clients = new List<UserAuth>();
                foreach (var firmUser in firmUsers)
                {
                    var attorneyClients = _userRepository.GetAttorneysClients(firmUser.Id).ToList().Where(x => x.CreatedDate >= monthAgo);
                    clients.AddRange(attorneyClients);
                }
                return clients.Distinct().Count();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve number of clients", ex);
            }
        }
    }
}
