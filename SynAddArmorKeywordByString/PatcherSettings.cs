using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis.Settings;
using StringCompareSettings;

namespace SynAddArmorKeywordByString
{
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
        public HashSet<StringCompareSetting>? StringsToSearch;
    }

    public class PatcherSettings
    {
        [SynthesisOrder]
        [SynthesisDiskName("AddKeywordTemplate")]
        [SynthesisSettingName("Keyword Template")]
        [SynthesisTooltip("Strings to search and keyword to set if found")]
        // public string BaselineMod { get; set; } = "MyFacegenBaseline.esp";
        public HashSet<KeywordData> AddKeywordTemplate = new();
    }
}
