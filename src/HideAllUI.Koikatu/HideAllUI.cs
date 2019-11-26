using BepInEx;
using BepInEx.Harmony;
using UnityEngine.SceneManagement;

namespace HideAllUI
{
    [BepInIncompatibility("HideStudioUI")]
    [BepInIncompatibility("HideHInterface")]
    [BepInPlugin(GUID, "HideAllUI", Version)]
    public class HideAllUI : HideAllUICore
    {
        protected override void Awake()
        {
            base.Awake();

            if(SceneManager.GetActiveScene().name == "StudioStart")
                HarmonyWrapper.PatchAll(typeof(HideStudioUI));
            else
                ThreadingHelper.Instance.StartAsyncInvoke(() => { HarmonyWrapper.PatchAll(typeof(HideHSceneUI)); return null; });
        }
    }
}
