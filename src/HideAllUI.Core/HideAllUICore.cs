using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;

namespace HideAllUI
{
    public abstract class HideAllUICore : BaseUnityPlugin
    {
        public const string GUID = "keelhauled.hideallui";
        public const string PluginName = "HideAllUI";
        public const string Version = "2.1.0";

        internal new static ManualLogSource Logger;
        internal static Harmony Harmony;

        // must be static for the transpiler
        internal static ConfigEntry<KeyboardShortcut> HideHotkey { get; set; }

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
    }
}
