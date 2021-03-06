﻿using System.Collections;

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
        public const string VERSION = "1.0.0";
        
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
                
                if (moreAccs && i == slots.transform.childCount - 1)
                    continue;

                if (!child.gameObject.activeSelf)
                {
                    text.text = $"スロット{i + 1:D2}";
                }
                else
                {
                    var acc = _char.charInfo.GetAccessory(i);
                    text.text = acc == null ? $"スロット{i+1:D2}" : $"{i+1:D2} {acc.GetComponent<AIChara.ListInfoComponent>().data.Name}";
                }
            }
        }
    }
}