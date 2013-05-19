using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class Addendum : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int HasAddendum { get; set; }
        public string AddendumDetails { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<AddendumViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Addendum)entity;
            UserId = updatingEntity.UserId;
            HasAddendum = updatingEntity.HasAddendum;
            AddendumDetails = updatingEntity.AddendumDetails;
        }

    }
}
