namespace Models.Contract
{
    public interface IFormEntity : IEntity       
    {
        long Id { get; set; }
        long UserId { get; set; }
        User User { get; set; }
        IViewModel ConvertToModel();
        void Update(IFormEntity entity);
    }
}
