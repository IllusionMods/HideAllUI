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

        private static bool HotkeyIsDown() => HideAllUICore.HideHotkey.Value.IsDown();

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

        [HarmonyTranspiler, HarmonyPatch(typeof(CustomControl), "Update")]
        private static IEnumerable<CodeInstruction> SetMakerHotkey(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            var inputGetKeyDown = AccessTools.Method(typeof(Input), nameof(Input.GetKeyDown), new Type[] { typeof(KeyCode) });

            for(int i = 0; i < codes.Count; i++)
            {
                if(codes[i].opcode == OpCodes.Ldc_I4_S && codes[i].operand is sbyte val && val == (sbyte)KeyCode.Space)
                {
                    if(codes[i + 1].opcode == OpCodes.Call && codes[i + 1].operand == inputGetKeyDown)
                    {
                        codes[i].opcode = OpCodes.Nop;
                        codes[i + 1] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(HideHSceneUI), nameof(HotkeyIsDown)));
                        break;
                    }
                }
            }

            return codes;
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
