using HarmonyLib;

using Studio;

namespace AI_StudioAccessoryNames
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl.AccessoriesInfo), "UpdateInfo")]
        private static void AccessoriesInfo_UpdateInfo_ChangeLabels(OCIChar _char)
        {
            AI_StudioAccessoryNames.instance.StartCoroutine(AI_StudioAccessoryNames.UpdateStudioLabelsDelayed(_char));
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl), "UpdateInfo")]
        private static void MPCharCtrl_UpdateInfo_ChangeLabels(MPCharCtrl __instance)
        {
            AI_StudioAccessoryNames.instance.StartCoroutine(AI_StudioAccessoryNames.UpdateStudioLabelsDelayed(__instance.ociChar));
        }
    }
}