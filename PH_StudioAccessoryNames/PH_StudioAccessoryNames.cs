using System.Collections;
using System.Linq;
using BepInEx;
using HarmonyLib;

using Studio;

using UnityEngine;
using UnityEngine.UI;

namespace PH_StudioAccessoryNames
{
    [BepInPlugin(nameof(PH_StudioAccessoryNames), nameof(PH_StudioAccessoryNames), VERSION)]
    [BepInProcess("PlayHomeStudio64bit")]
    [BepInProcess("PlayHomeStudio32bit")]
    public class PH_StudioAccessoryNames : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";
        
        public static PH_StudioAccessoryNames instance;

        private void Awake()
        {
            instance = this;

            var harmony = new Harmony("PH_StudioAccessoryNames");
            harmony.PatchAll(typeof(Hooks));
        }

        public static IEnumerator UpdateStudioLabelsDelayed(OCIChar _char)
        {
            yield return null;

            var slots = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/01_State/Viewport/Content/Slot");

            var index = 0;
            for (var i = 0; i < slots.transform.childCount; i++)
            {
                var child = slots.transform.GetChild(i);

                var buttons = child.GetComponentsInChildren<Button>();
                foreach (var btn in buttons)
                {
                    var oldPos = btn.transform.localPosition;

                    switch ((int)oldPos.x)
                    {
                        case 80:
                            btn.transform.localPosition = new Vector3(104, oldPos.y, oldPos.z);
                            break;
                        case 104:
                            btn.transform.localPosition = new Vector3(128, oldPos.y, oldPos.z);
                            break;
                    }
                }
                
                var text = child.GetComponentInChildren<Text>();
                if (text == null)
                    continue;

                text.resizeTextMinSize = 6;

                var textRect = text.GetComponent<RectTransform>();
                textRect.offsetMax = new Vector2(100, textRect.offsetMax.y);
                
                if (!text.text.Any(char.IsDigit))
                    continue;

                if (!child.gameObject.activeSelf)
                {
                    text.text = $"スロット{index + 1:D2}";
                }
                else
                {
                    var acc = _char.charInfo.human.accessories.GetAccessoryData(_char.charInfo.human.customParam.acce, index);
                    text.text = acc == null ? $"スロット{index + 1:D2}" : $"{index + 1:D2} {acc.name}";
                }

                index++;
            }
        }
    }
}