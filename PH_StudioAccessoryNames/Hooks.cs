using HarmonyLib;

using Studio;

namespace PH_StudioAccessoryNames
{
    public static class Hooks
    {
        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl.AccessoriesInfo), "UpdateInfo")]
        private static void AccessoriesInfo_UpdateInfo_ChangeLabels(OCIChar _char) => PH_StudioAccessoryNames.instance.StartCoroutine(PH_StudioAccessoryNames.UpdateStudioLabelsDelayed(_char));

        [HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl), "UpdateInfo")]
        private static void MPCharCtrl_UpdateInfo_ChangeLabels(OCIChar ___m_OCIChar) => PH_StudioAccessoryNames.instance.StartCoroutine(PH_StudioAccessoryNames.UpdateStudioLabelsDelayed(___m_OCIChar));
    }
}