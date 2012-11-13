﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyForms.Models
{
    public class AllFormsViewModel
    {
        public bool IsStarterDone { get; set; }
        public bool IsParentingDone { get; set; }
        public bool IsDomesticDone { get; set; }
        public bool IsFinancial { get; set; }
        public bool HasChildren { get; set; }
    }
}