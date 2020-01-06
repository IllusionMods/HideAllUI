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
        private IEnumerable<Canvas> canvasList;
        private bool visible = true;

        public HideHSceneUI()
        {
            canvasList = GameObject.FindObjectsOfType<Canvas>().Where(x => x.name == "Canvas");
        }

        public override void ToggleUI()
        {
            visible = !visible;
            foreach(var canvas in canvasList.Where(x => x))
                canvas.enabled = visible;
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
