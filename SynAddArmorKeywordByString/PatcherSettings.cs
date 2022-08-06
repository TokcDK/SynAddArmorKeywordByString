using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using StringCompareSettings;

namespace SynAddArmorKeywordByString
{
    public enum SearchMethod
    {
        OR,
        AND
    }

    [SynthesisObjectNameMember("Keyword data")]
    public class KeywordData
    {
        [SynthesisOrder]
        [SynthesisDiskName("Keyword")]
        [SynthesisTooltip("Keyword to set if found by strings")]
        public FormLink<IKeywordGetter>? Keyword;
        [SynthesisOrder]
        [SynthesisDiskName("StringsToSearch")]
        [SynthesisTooltip("Strings for keyword")]
        public HashSet<StringCompareSetting> StringsToSearch = new();
        [SynthesisOrder]
        [SynthesisDiskName("StringsToSearch")]
        [SynthesisTooltip("Strings serach method. OR=any, AND=all")]
        public SearchMethod StringsSearchMethod = SearchMethod.OR;
    }

    public class PatcherSettings
    {
        [SynthesisOrder]
        [SynthesisDiskName("AddKeywordTemplate")]
        [SynthesisSettingName("Keyword Templates")]
        [SynthesisTooltip("Strings to search and keyword to set if found")]
        // public string BaselineMod { get; set; } = "MyFacegenBaseline.esp";
        public HashSet<KeywordData> AddKeywordTemplate = new();
    }
}
