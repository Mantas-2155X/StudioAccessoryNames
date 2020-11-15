using HarmonyLib;

using Studio;

namespace KK_StudioAccessoryNames
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl.AccessoriesInfo), "UpdateInfo")]
        private static void AccessoriesInfo_UpdateInfo_ChangeLabels(OCIChar _char)
        {
            KK_StudioAccessoryNames.instance.StartCoroutine(KK_StudioAccessoryNames.UpdateStudioLabelsDelayed(_char));
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl), "UpdateInfo")]
        private static void MPCharCtrl_UpdateInfo_ChangeLabels(MPCharCtrl __instance)
        {
            KK_StudioAccessoryNames.instance.StartCoroutine(KK_StudioAccessoryNames.UpdateStudioLabelsDelayed(__instance.ociChar));
        }
    }
}