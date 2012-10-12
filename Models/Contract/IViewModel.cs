namespace Models.Contract
{
    public interface IViewModel
    {        
        int UserId { get; set; }
        IFormEntity ConvertToEntity();
    }
}
