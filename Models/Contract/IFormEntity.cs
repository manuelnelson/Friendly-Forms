namespace Models.Contract
{
    public interface IFormEntity        
    {
        int Id { get; set; }
        int UserId { get; set; }
        IViewModel ConvertToModel();
        void Update(IFormEntity entity);
    }
}
