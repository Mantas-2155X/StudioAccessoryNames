using HarmonyLib;

using Studio;

namespace HS2_StudioAccessoryNames
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl.AccessoriesInfo), "UpdateInfo")]
        private static void AccessoriesInfo_UpdateInfo_ChangeLabels(OCIChar _char)
        {
            HS2_StudioAccessoryNames.instance.StartCoroutine(HS2_StudioAccessoryNames.UpdateStudioLabelsDelayed(_char));
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl), "UpdateInfo")]
        private static void MPCharCtrl_UpdateInfo_ChangeLabels(MPCharCtrl __instance)
        {
            HS2_StudioAccessoryNames.instance.StartCoroutine(HS2_StudioAccessoryNames.UpdateStudioLabelsDelayed(__instance.ociChar));
        }
    }
}