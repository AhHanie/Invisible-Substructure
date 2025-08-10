
using HarmonyLib;
using Verse;

namespace SK_No_Substructue_Render
{
    public class Mod: Verse.Mod
    {
        public static Harmony instance;

        public Mod(ModContentPack content): base(content)
        {
            instance = new Harmony("rimworld.sk.nosubstructurerender");

            LongEventHandler.QueueLongEvent(Init, "SK_No_Substructue_Render.Init", true, null);
        }

        public void Init()
        {
            HarmonyPatcher.PatchAllVanillaMethods(instance);
        }
    }
}
