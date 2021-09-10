using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection.Emit;
using System.Reflection;

namespace HideAllUI
{
    public abstract class HideAllUICore : BaseUnityPlugin
    {
        public const string GUID = "keelhauled.hideallui";
        public const string PluginName = "HideAllUI";
        public const string Version = "2.3";

        internal new static ManualLogSource Logger;
        internal static Harmony Harmony;

        // must be static for the transpiler
        internal static ConfigEntry<KeyboardShortcut> HideHotkey { get; set; }
        private static bool HotkeyIsDown() => HideHotkey.Value.IsDown();

        internal static HideUIAction currentUIHandler;

        protected virtual void Awake()
        {
            Harmony = new Harmony($"{GUID}.harmony");
            Logger = base.Logger;
            HideHotkey = Config.Bind("Keyboard shortcuts", "Hide UI", new KeyboardShortcut(KeyCode.Space));
        }

        private void Update()
        {
            if(currentUIHandler != null && HideHotkey.Value.IsDown())
                currentUIHandler.ToggleUI();
        }

#if DEBUG
        private void OnDestroy()
        {
            Harmony.UnpatchAll();
        } 
#endif

        internal static IEnumerable<CodeInstruction> HideHotkeyHook(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            var inputGetKeyDown = AccessTools.Method(typeof(Input), nameof(Input.GetKeyDown), new Type[] { typeof(KeyCode) });

            for(int i = 0; i < codes.Count; i++)
            {
                if(codes[i].opcode == OpCodes.Ldc_I4_S && codes[i].operand is sbyte val && val == (sbyte)KeyCode.Space)
                {
                    if(codes[i + 1].opcode == OpCodes.Call && codes[i + 1].operand is MethodInfo methodInfo && methodInfo == inputGetKeyDown)
                    {
                        codes[i].opcode = OpCodes.Nop;
                        codes[i + 1] = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(HideAllUICore), nameof(HotkeyIsDown)));
                        break;
                    }
                }
            }

            return codes;
        }
    }
}
