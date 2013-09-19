using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessLogic.Contracts;
using BusinessLogic.Properties;

namespace BusinessLogic
{
    public class ConstantsService : IConstantsService
    {
        public Dictionary<string, string> GetConstants()
        {
            var rm = Resources.ResourceManager;
            var rs = rm.GetResourceSet(new CultureInfo("en-US"), true, true);
            var dictionary = new Dictionary<string, string>();
            if (rs != null)
            {
                var constants = rs.Cast<DictionaryEntry>().ToList();
                foreach (var dictionaryEntry in constants)
                {
                    dictionary.Add(dictionaryEntry.Key.ToString(),dictionaryEntry.Value.ToString());
                }
                //Remove sensitive items here
                dictionary.Remove("FromEmail");
                dictionary.Remove("FromPassword");
                dictionary.Remove("MailServerName");
            }
            return dictionary;
        } 
    }
}
