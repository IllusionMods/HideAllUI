using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace HideAllUI
{
    [BepInPlugin(GUID, PluginName, Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();
            Harmony.PatchAll(typeof(Hooks));
        }

        private class Hooks
        {
            [HarmonyPostfix, HarmonyPatch(typeof(CharaCustom.CharaCustom), "Start")]
            public static void MakerEntrypoint()
            {
                currentUIHandler = new HideMakerUI();
            }
            
            [HarmonyPostfix, HarmonyPatch(typeof(CharaCustom.CharaCustom), "OnDestroy")]
            public static void MakerEnd()
            {
                currentUIHandler = null;
            }
            
            [HarmonyPostfix, HarmonyPatch(typeof(Studio.Studio), "Awake")]
            public static void StudioEntrypoint()
            {
                currentUIHandler = new HideStudioUI();
            }
            
            [HarmonyPrefix, HarmonyPatch(typeof(HScene), "SetStartVoice")]
            public static void HSceneStart(HScene __instance)
            {
                var traverse = Traverse.Create(__instance);
            
                CanvasGroup UIGroup = traverse.Field("sprite").Field("UIGroup").GetValue<CanvasGroup>();
                if (UIGroup == null)
                    return;
                
                currentUIHandler = new HideHSceneUI(UIGroup);
            }

            [HarmonyPrefix, HarmonyPatch(typeof(HScene), "OnDisable")]
            public static void HSceneEnd()
            {
                currentUIHandler = null;
            }
        }
    }
}
