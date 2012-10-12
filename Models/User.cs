using System;

namespace Models
{
    public class User
    {
        public User()
        {
            CreatedDate = DateTime.UtcNow;
            FailedPasswordAttemptStart = DateTime.UtcNow.AddMonths(-1);
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptStart { get; set; }
        public bool Verified { get; set; }

    }
}
