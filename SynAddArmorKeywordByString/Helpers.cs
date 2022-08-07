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
        public static IEnumerable<KeywordData?> GetAllValues(this string edid, HashSet<KeywordData> listOfTemplates)
        {
            foreach(var template in listOfTemplates)
            {
                if (template==null) continue;
                if (template.Keyword==null) continue;
                if (template.Keyword.FormKey.IsNull) continue;

                if (template.StringsSearchMethod == SearchMethod.AND)
                {
                    if (!edid.HasAllFromList(template!.StringsToSearch!, template.StringsBlacklist)) continue;
                }
                else if (!edid.HasAnyFromList(template!.StringsToSearch!, template.StringsBlacklist)) continue;

                yield return template;
            }
        }
    }
}
