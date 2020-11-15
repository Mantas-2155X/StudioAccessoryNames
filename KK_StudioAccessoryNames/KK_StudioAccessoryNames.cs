using System.Collections;

using BepInEx;
using HarmonyLib;

using KKAPI.Maker;

using Studio;

using UnityEngine;
using UnityEngine.UI;

namespace KK_StudioAccessoryNames
{
    [BepInPlugin(nameof(KK_StudioAccessoryNames), nameof(KK_StudioAccessoryNames), VERSION)]
    public class KK_StudioAccessoryNames : BaseUnityPlugin
    {
        public const string VERSION = "1.0.0";
        
        public static KK_StudioAccessoryNames instance;

        private void Awake()
        {
            instance = this;

            var harmony = new Harmony("KK_StudioAccessoryNames");
            harmony.PatchAll(typeof(Hooks));
        }

        public static IEnumerator UpdateStudioLabelsDelayed(OCIChar _char)
        {
            yield return null;

            var slots = GameObject.Find("StudioScene/Canvas Main Menu/02_Manipulate/00_Chara/01_State/Viewport/Content/Slot");
            var moreAccs = slots.transform.childCount > 20;
            
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
                
                if (moreAccs && i < 3)
                    continue;

                if (!child.gameObject.activeSelf)
                {
                    text.text = $"スロット{i+(moreAccs ? -2 : 1):D2}";
                }
                else
                {
                    var acc = _char.charInfo.GetAccessory(i+(moreAccs ? -3 : 0));
                    text.text = acc == null ? $"スロット{i+(moreAccs ? -2 : 1):D2}" : $"{i+(moreAccs ? -2 : 1):D2} {acc.GetComponent<ListInfoComponent>().data.Name}";
                }
            }
        }
    }
}