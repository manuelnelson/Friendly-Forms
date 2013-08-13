using System.Collections.Generic;
using Models;
using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class ChildAllViewModel
    {
        public List<Child> ChildViewModel { get; set; }
        public ChildFormViewModel ChildFormViewModel { get; set; }
    }
}