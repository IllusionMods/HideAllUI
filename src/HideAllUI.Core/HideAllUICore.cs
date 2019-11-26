using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace HideAllUI
{
    public abstract class HideAllUICore : BaseUnityPlugin
    {
        public const string GUID = "keelhauled.hideallui";
        public const string Version = "2.0";

        // must be static for the transpiler
        internal static ConfigEntry<KeyboardShortcut> HideHotkey { get; set; }

        internal static HideUIAction currentUIHandler;

        protected virtual void Awake()
        {
            HideHotkey = Config.Bind("Keyboard shortcuts", "Hide UI", new KeyboardShortcut(KeyCode.Space));
        }

        private void Update()
        {
            if(currentUIHandler != null && HideHotkey.Value.IsDown())
                currentUIHandler.ToggleUI();
        }
    }
}
