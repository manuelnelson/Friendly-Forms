using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            subMenuItems = new List<FormMenuItem>();
        }
        public string itemClass { get; set; }
        public string path { get; set; }
        public string pathIdentifier { get; set; }
        public string iconClass { get; set; }
        public string text { get; set; }
        public bool showSubMenu { get; set; }
        public string formName { get; set; }

        public List<FormMenuItem> subMenuItems { get; set; }
    }
}