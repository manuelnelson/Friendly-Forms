namespace Models.Contract
{
    public interface IChildFormEntity : IFormEntity
    {
        long ChildId { get; set; }
        Child Child { get; set; }
    }
}
