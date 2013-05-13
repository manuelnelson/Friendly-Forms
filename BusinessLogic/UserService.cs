﻿using System;
using BusinessLogic.Contracts;
using DataInterface;
using Elmah;
using Models;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService)
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
    }
}
