namespace Models.Contract
{
    public interface IFormEntity        
    {
        long Id { get; set; }
        int UserId { get; set; }
        IViewModel ConvertToModel();
        void Update(IFormEntity entity);
    }
}
