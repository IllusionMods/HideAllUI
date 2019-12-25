using BepInEx;
using BepInEx.Harmony;

namespace HideAllUI
{
    [BepInProcess("PlayHomeStudio64bit")]
    [BepInProcess("PlayHomeStudio32bit")]
    [BepInPlugin(GUID, PluginName, Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();
            HarmonyWrapper.PatchAll(typeof(HideStudioUI));
        }
    }
}
