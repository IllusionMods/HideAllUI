using BepInEx;
using BepInEx.Harmony;
using HarmonyLib;

namespace HideAllUI
{
    [BepInPlugin(GUID, PluginName, Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();

            if(Paths.ProcessName == "PlayHomeStudio64bit" || Paths.ProcessName == "PlayHomeStudio32bit")
                HarmonyWrapper.PatchAll(typeof(HideStudioUI));
            else
                Harmony.Patch(typeof(GameControl).GetMethod("Update_Key", AccessTools.all),
                              transpiler: new HarmonyMethod(typeof(HideAllUICore).GetMethod(nameof(HideAllUICore.HideHotkeyHook), AccessTools.all)));
        }
    }
}
