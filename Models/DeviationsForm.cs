using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class DeviationsForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
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
