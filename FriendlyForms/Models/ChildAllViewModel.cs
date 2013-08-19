using System.Collections.Generic;
using System.Runtime.Serialization;
using Models;
using Models.ViewModels;

namespace FriendlyForms.Models
{
    [DataContract]
    public class ChildAllViewModel
    {
        [DataMember]
        public List<Child> ChildViewModel { get; set; }
        [DataMember]
        public ChildFormViewModel ChildFormViewModel { get; set; }
    }
}