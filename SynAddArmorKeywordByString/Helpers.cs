using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCompareSettings;

namespace SynAddArmorKeywordByString
{
    internal static class Helpers
    {
        public static bool TryGetValue(this string edid, HashSet<KeywordData> listOfTemplates, out KeywordData? keywordData)
        {
            foreach(var template in listOfTemplates)
            {
                if (template==null) continue;
                if (template.Keyword==null) continue;
                if (template.Keyword.FormKey.IsNull) continue;
                if (!edid.IsInList(template!.StringsToSearch!)) continue;

                keywordData = template;
                return true;
            }

            keywordData = null;
            return false;
        }
    }
}
