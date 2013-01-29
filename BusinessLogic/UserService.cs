using System;
using BusinessLogic.Contracts;
using BusinessLogic.Models;
using DataInterface;
using DevOne.Security.Cryptography.BCrypt;
using Elmah;
using Models;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _salt;
        private readonly IMailService _mailService;
        private const int MaxNumberOfPasswordFailedAttempts = 10;
        private const int NumberOfMinutesBeforeFailedPasswordReset = -10;
        public UserService(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
            _salt = BCryptHelper.GenerateSalt(6);
        }

        public bool DoesNativeUserExist(string email)
        {
            return _userRepository.NativeExists(email);
        }

        public AccountUser CreateNativeUser(string email, string firstName, string lastName, string password, int roleId = (int)Role.Default)
        {
            try
            {
                //The salt should be a number between 4-31. The greater the number, the greater the work factor, the longer
                //it takes to encrypt (good for security, though will have an effect on performance)
                var hashedPwd = BCryptHelper.HashPassword(password, _salt);
                var user = new User
                {
                    Email = email,
                    Password = hashedPwd,
                    FirstName = firstName,
                    LastName = lastName,
                    RoleId = roleId
                };
                _userRepository.Add(user);
                var hashedEmail = BCryptHelper.HashPassword(email, _salt);
                _mailService.SendVerificationEmail(email, hashedEmail);
                return user.TranslateTo<AccountUser>();                
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new BusinessServicesException("Unable to create the user", ex);
            }

        }

        public AccountUser Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            //user doesn't exist. return null.
            if (user == null)
                return null;

            if (user.FailedPasswordAttemptCount >= MaxNumberOfPasswordFailedAttempts)
            {
                throw new BusinessServicesException("Too many invalid attempts.  Please wait 10 minutes before trying to log back in.");
            }
            if (BCryptHelper.CheckPassword(password, user.Password))
            {
                user.RoleId = user.RoleId ?? (int)Role.Default;
                return user.TranslateTo<AccountUser>();
            }
                

            //Failed password attempt.  Record
            AddFailedPasswordAttempt(user);

            //if we made it this far, something failed
            return null;
        }

        private void AddFailedPasswordAttempt(User user)
        {
            if (DateTime.Compare(user.FailedPasswordAttemptStart, DateTime.UtcNow.AddMinutes(NumberOfMinutesBeforeFailedPasswordReset)) < 0)
            {
                //It's been more than 10 minutes since last failed password attempt.  Reset
                user.FailedPasswordAttemptStart = DateTime.UtcNow;
                user.FailedPasswordAttemptCount = 1;
                _userRepository.Update(user);
            }
            else
            {
                //User has attempted in the past ten minutes.  Increment
                user.FailedPasswordAttemptCount++;
                _userRepository.Update(user);
            }
        }
    }
}
