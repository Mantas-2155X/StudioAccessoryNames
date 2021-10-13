using System.Collections;
using System.Linq;
using AIChara;
using BepInEx;
using HarmonyLib;

using KKAPI.Maker;

using Studio;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace AI_StudioAccessoryNames
{
    [BepInPlugin(nameof(AI_StudioAccessoryNames), nameof(AI_StudioAccessoryNames), VERSION)]
    [BepInProcess("StudioNEOV2")]
    public class AI_StudioAccessoryNames : BaseUnityPlugin
    {
        public const string VERSION = "1.1.1";
        
        public static AI_StudioAccessoryNames instance;

        private void Awake()
        {
            instance = this;

            var harmony = new Harmony("AI_StudioAccessoryNames");
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

                    switch (oldPos.x)
                    {
                        case 100:
                            btn.transform.localPosition = new Vector3(155, oldPos.y, oldPos.z);
                            break;
                        case 130:
                            btn.transform.localPosition = new Vector3(185, oldPos.y, oldPos.z);
                            break;
                    }
                }
                
                var text = child.GetComponentInChildren<TextMeshProUGUI>();
                if (text == null)
                    continue;

                var textRect = text.GetComponent<RectTransform>();
                textRect.offsetMax = new Vector2(150, textRect.offsetMax.y);
                
                if (!text.text.Any(char.IsDigit))
                    continue;

                if (!child.gameObject.activeSelf)
                {
                    text.text = $"スロット{index + 1:D2}";
                }
                else
                {
                    var accObj = _char.charInfo.GetAccessoryObject(index);
                    if (accObj != null)
                    {
                        var acc = accObj.GetComponent<CmpAccessory>();
                        text.text = acc == null ? $"スロット{index + 1:D2}" : $"{index + 1:D2} {acc.GetComponent<ListInfoComponent>().data.Name}";
                    }
                    else
                    {
                        text.text = $"スロット{index + 1:D2}";
                    }
                }

                index++;
            }
        }
    }
}