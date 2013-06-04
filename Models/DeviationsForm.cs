using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("DeviationsForms")]
    public class DeviationsForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public bool IsOtherParent { get; set; }
        public int Deviation { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DeviationsFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
