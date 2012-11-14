using Models.Contract;

namespace Models.ViewModels
{
    public class ChildFormViewModel : IViewModel
    {
        public int Id { get; set; }
        public int ChildrenInvolved { get; set; }

        public int UserId { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new ChildForm()
            {
                Id = Id,
                UserId = UserId,
                ChildrenInvolved = ChildrenInvolved
            };
        }

    }
}
