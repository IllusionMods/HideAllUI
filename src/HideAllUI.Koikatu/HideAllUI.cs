using BepInEx;
using BepInEx.Harmony;
using ChaCustom;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace HideAllUI
{
    [BepInIncompatibility("HideStudioUI")]
    [BepInIncompatibility("HideHInterface")]
    [BepInPlugin(GUID, PluginName, Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();

            if(SceneManager.GetActiveScene().name == "StudioStart")
                HarmonyWrapper.PatchAll(typeof(HideStudioUI));
            else
            {
                HarmonyWrapper.PatchAll(typeof(HideHSceneUI));
                Harmony.Patch(typeof(CustomControl).GetMethod("Update", AccessTools.all),
                              transpiler: new HarmonyMethod(typeof(HideAllUICore).GetMethod(nameof(HideAllUICore.HideHotkeyHook), AccessTools.all)));
            }
        }
    }
}
