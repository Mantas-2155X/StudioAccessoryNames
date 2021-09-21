using HarmonyLib;

using Studio;

namespace KKS_StudioAccessoryNames
{
	public static class Hooks
	{
		[HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl.AccessoriesInfo), "UpdateInfo")]
		private static void AccessoriesInfo_UpdateInfo_ChangeLabels(OCIChar _char)
		{
			KKS_StudioAccessoryNames.instance.StartCoroutine(KKS_StudioAccessoryNames.UpdateStudioLabelsDelayed(_char));
		}
        
		[HarmonyPostfix, HarmonyPatch(typeof(MPCharCtrl), "UpdateInfo")]
		private static void MPCharCtrl_UpdateInfo_ChangeLabels(MPCharCtrl __instance)
		{
			KKS_StudioAccessoryNames.instance.StartCoroutine(KKS_StudioAccessoryNames.UpdateStudioLabelsDelayed(__instance.ociChar));
		}
	}
}