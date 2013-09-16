using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Communications")]
    public class Communication : IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int AllowCommunication { get; set; }
        public bool Telephone { get; set; }
        public bool Email { get; set; }
        public bool Other { get; set; }
        public string OtherMethod { get; set; }
        public int Limitations { get; set; }
        public string LimitationDetails { get; set; }
        public int Notification { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
           throw new NotImplementedException();
        }


    }
}
