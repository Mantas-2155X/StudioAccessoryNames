using System.Collections;
using System.Linq;
using BepInEx;
using HarmonyLib;

using KKAPI.Maker;

using Studio;

using UnityEngine;
using UnityEngine.UI;

namespace KKS_StudioAccessoryNames
{
    [BepInPlugin(nameof(KKS_StudioAccessoryNames), nameof(KKS_StudioAccessoryNames), VERSION)]
    [BepInProcess("CharaStudio")]
    public class KKS_StudioAccessoryNames : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";
        
        public static KKS_StudioAccessoryNames instance;

        private void Awake()
        {
            instance = this;

            var harmony = new Harmony("KKS_StudioAccessoryNames");
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
                            btn.transform.localPosition = new Vector3(160, oldPos.y, oldPos.z);
                            break;
                        case 130:
                            btn.transform.localPosition = new Vector3(190, oldPos.y, oldPos.z);
                            break;
                    }
                }
                
                var text = child.GetComponentInChildren<Text>();
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
                        var acc = accObj.GetComponent<ChaAccessoryComponent>();
                        text.text = acc == null ? $"スロット{index + 1:D2}" : $"{index + 1:D2} {acc.GetComponent<ListInfoComponent>().data.Name}";
                    }
                }

                index++;
            }
        }
    }
}