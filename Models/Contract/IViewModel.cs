namespace Models.Contract
{
    public interface IViewModel
    {
        long UserId { get; set; }
        IFormEntity ConvertToEntity();
    }
}
