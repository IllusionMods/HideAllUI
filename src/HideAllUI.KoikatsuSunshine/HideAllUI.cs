using BepInEx;
using ChaCustom;
using HarmonyLib;

namespace HideAllUI
{
    [BepInPlugin(GUID, PluginName, Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();

            if(UnityEngine.Application.productName == "CharaStudio")
                Harmony.PatchAll(typeof(HideStudioUI));
            else
            {
                Harmony.PatchAll(typeof(HideHSceneUI));
                Harmony.Patch(typeof(CustomControl).GetMethod("Update", AccessTools.all),
                              transpiler: new HarmonyMethod(typeof(HideAllUICore).GetMethod(nameof(HideAllUICore.HideHotkeyHook), AccessTools.all)));
            }
        }
    }
}
