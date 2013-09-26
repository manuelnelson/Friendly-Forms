using System;
using Models.Contract;
using ServiceStack.DataAnnotations;

namespace Models
{
    [Alias("OtherChildrens")]
    public class OtherChildren : IFormEntity
    {
        [AutoIncrement]
        public virtual long Id { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public virtual int LegallyResponsible { get; set; }
        public virtual int AtHome { get; set; }
        public virtual int Support { get; set; }
        public virtual int Preexisting { get; set; }
        public virtual int InCourt { get; set; }
        public virtual string Details { get; set; }

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
