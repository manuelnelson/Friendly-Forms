using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Models;
using Models;
using Models.ViewModels;

namespace BusinessLogic.Helpers
{
    public static class PrivacyHelper
    {
        public static PrivacyViewModel ConvertToModel(this Privacy privacy)
        {
            return new PrivacyViewModel()
                {
                    Details = privacy.Details,
                    NeedPrivacy = privacy.NeedPrivacy,
                    UserId = privacy.UserId
                };
        }
    }
}
