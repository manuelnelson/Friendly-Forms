using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class MenuItem
    {
        public string itemClass { get; set; }
        public string path {get;set;}
        public string iconClass { get; set; }
        public string text { get; set; }
        public bool showSubMenu { get; set; }
        public string formName { get; set; }
        public List<MenuItem> subMenuItems { get; set; }
    }
}