namespace Models.Contract
{
    public interface IFormEntity : IEntity       
    {
        long UserId { get; set; }
        User User { get; set; }
        bool IsValid();
        void Update(IFormEntity entity);
    }
}
