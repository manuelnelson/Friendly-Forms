namespace BusinessLogic.Models
{
    public class FormMenuItem
    {
        public string itemClass { get; set; }
        public string path { get; set; }
        public string pathIdentifier { get; set; }
        public string iconClass { get; set; }
        public string text { get; set; }
        public string formName { get; set; }
        public bool disabled { get; set; }
        public long userId { get; set; }
        public bool hasChildren { get; set; }
        public bool isComplete { get; set; }
        public bool hasLink { get; set; }
    }
}
