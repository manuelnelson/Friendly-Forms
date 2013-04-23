using System;
using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("Users")]
    public class User : IEntity
    {
        public User()
        {
            CreatedDate = DateTime.UtcNow;
            FailedPasswordAttemptStart = DateTime.UtcNow.AddMonths(-1);
            RoleId = 4;
        }
        [AutoIncrement]
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptStart { get; set; }
        public bool Verified { get; set; }
        public int? RoleId { get; set; } //1=Admin, 2=firm admin, 3=lawyer, 4 or null=client(default)
    }
}
