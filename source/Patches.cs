using RimWorld;
using Verse;

namespace SK_No_Substructue_Render
{
    public class Patches
    {
        private static bool _isRenderingTerrain = false;
        public static void ShouldDrawPropsOnPostfixPatch(IntVec3 c, TerrainGrid terrGrid, ref bool __result)
        {
            TerrainDef terrainDef = terrGrid.FoundationAt(c);
            if (terrainDef == null)
            {
                return;
            }
            SubstructureTerrainDefModExtension ext  = terrainDef.GetModExtension<SubstructureTerrainDefModExtension>();
            if (ext == null)
            {
                return;
            }
            __result = !ext.transparent;
        }

        public static void SectionLayer_Terrain_Regenerate_Prefix()
        {
            _isRenderingTerrain = true;
        }

        public static void SectionLayer_Terrain_Regenerate_Postfix()
        {
            _isRenderingTerrain = false;
        }

        public static void TerrainAtPostfixPatch(ref TerrainDef __result, TerrainGrid __instance, IntVec3 c)
        {
            // Only affect terrain during rendering, not during gameplay logic
            if (!_isRenderingTerrain || __result == null)
            {
                return;
            }

            SubstructureTerrainDefModExtension ext = __result.GetModExtension<SubstructureTerrainDefModExtension>();
            if (ext == null)
            {
                return;
            }

            if (ext.transparent) 
            {
                __result = __instance.BaseTerrainAt(c);
            }
        }
    }
}
