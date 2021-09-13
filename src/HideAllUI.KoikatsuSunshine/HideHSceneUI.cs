using ChaCustom;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace HideAllUI
{
    internal class HideHSceneUI : HideUIAction
    {
        private bool visible = true;

        public override void ToggleUI()
        {
            visible = !visible;
            GameObject.FindObjectOfType<HSprite>().GetComponent<Canvas>().enabled = visible;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HSceneProc), "SetShortcutKey")]
        private static void HSceneStart()
        {
            HideAllUICore.currentUIHandler = new HideHSceneUI();
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HSceneProc), "OnDestroy")]
        private static void HSceneEnd()
        {
            HideAllUICore.currentUIHandler = null;
        }
    }
}
