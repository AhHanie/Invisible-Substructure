using HarmonyLib;
using RimWorld;
using System;
using System.Reflection;
using Verse;

namespace SK_No_Substructue_Render
{
    public class HarmonyPatcher
    {
        public static void PatchAllVanillaMethods(Harmony instance)
        {
            // Patch SectionLayer_SubstructureProps.ShouldDrawPropsOn method
            MethodInfo shouldDrawPropsMethod = AccessTools.Method(typeof(SectionLayer_SubstructureProps), "ShouldDrawPropsOn");
            HarmonyMethod shouldDrawPropsOnPostfixPatch = new HarmonyMethod(typeof(Patches).GetMethod("ShouldDrawPropsOnPostfixPatch"));
            instance.Patch(shouldDrawPropsMethod, null, shouldDrawPropsOnPostfixPatch);

            // Patch SectionLayer_Terrain.Regenerate
            MethodInfo regenerateMethod = AccessTools.Method(typeof(SectionLayer_Terrain), "Regenerate");
            HarmonyMethod terrainRegeneratePrefix = new HarmonyMethod(typeof(Patches).GetMethod("SectionLayer_Terrain_Regenerate_Prefix"));
            HarmonyMethod terrainRegeneratePostfix = new HarmonyMethod(typeof(Patches).GetMethod("SectionLayer_Terrain_Regenerate_Postfix"));
            instance.Patch(regenerateMethod, terrainRegeneratePrefix, terrainRegeneratePostfix);

            // Patch TerrainGrid.TerrainAt method only during terrain rendering
            MethodInfo terrainAtMethod = AccessTools.Method(typeof(TerrainGrid), "TerrainAt", new Type[] { typeof(IntVec3) });
            HarmonyMethod terrainAtPostfixPatch = new HarmonyMethod(typeof(Patches).GetMethod("TerrainAtPostfixPatch"));
            instance.Patch(terrainAtMethod, null, terrainAtPostfixPatch);
        }
    }
}
