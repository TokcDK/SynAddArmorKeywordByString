using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using StringCompareSettings;

namespace SynAddArmorKeywordByString
{
    public class Program
    {
        static Lazy<PatcherSettings> Settings = null!;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("PatcherSettings", "settings.json", out Settings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "YourPatcher.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            // set local vars
            if(!Settings.Value.AddKeywordTemplate.Any(t => t.Keyword != null && !t.Keyword.FormKey.IsNull && t.StringsToSearch.Count > 0 && t.StringsToSearch.Any(s => !string.IsNullOrWhiteSpace(s.Name))))
            {
                Console.WriteLine($"No any keyword/strings is set. Try to enter any and run the patch again.");
                return;
            }

            var listOfTemplates = Settings.Value.AddKeywordTemplate;

            foreach (var armorGetter in state.LoadOrder.PriorityOrder.Armor().WinningOverrides())
            {
                // skip invalid
                if (armorGetter == null) continue;
                if (string.IsNullOrWhiteSpace(armorGetter.EditorID)) continue;
                if (armorGetter.Keywords==null) continue;
                //if (armorGetter.MajorFlags.HasFlag(Armor.MajorFlag.NonPlayable)) continue;
                //if (armorGetter.MajorFlags.HasFlag(Armor.MajorFlag.Shield)) continue;
                //if (armorGetter.BodyTemplate == null) continue;
                //if (armorGetter.BodyTemplate.Flags.HasFlag(BodyTemplate.Flag.NonPlayable)) continue;
                if (!armorGetter.EditorID.TryGetValue(listOfTemplates, out var keywordData) || keywordData==null) continue;
                if (armorGetter.Keywords.Contains(keywordData.Keyword)) continue;

                Console.WriteLine($"Add keyword {keywordData.Keyword!.FormKey} for {armorGetter.EditorID}|{armorGetter.FormKey}");
                var armorEdit = state.PatchMod.Armors.GetOrAddAsOverride(armorGetter);
                //if (armorEdit.Keywords == null) armorEdit.Keywords = new Noggog.ExtendedList<Mutagen.Bethesda.Plugins.IFormLinkGetter<IKeywordGetter>>();
                armorEdit.Keywords!.Add(keywordData.Keyword!);
            }
        }
    }
}
