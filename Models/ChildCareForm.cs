using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ChildCareForms")]
    public class ChildCareForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ChildrenInvolved { get; set; }
        [Ignore]
        public virtual User User { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ChildCareFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }


    }
}
