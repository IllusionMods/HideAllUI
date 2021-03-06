using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HideAllUI
{
    internal class HideStudioUI : HideUIAction
    {
        private string[] gameCanviNames = { "Canvas", "Canvas Object List", "Canvas Main Menu", "Canvas System Menu", "Canvas Guide Input", "Canvas Color", "Canvas Pattern" };
        private string[] pluginCanviNames = { "KKPECanvas(Clone)", "BepInEx_Manager/MaterialEditorCanvas", "QuickAccessBoxCanvas(Clone)" };

        private IEnumerable<Canvas> canvasList;

        private bool visible = true;

        public HideStudioUI()
        {
            canvasList = GameObject.FindObjectsOfType<Canvas>().Where(x => gameCanviNames.Contains(x.name));
        }

        public override void ToggleUI()
        {
            visible = !visible;
            foreach (var canvas in canvasList.Where(x => x))
                canvas.gameObject.SetActive(visible);

            foreach (var objectName in pluginCanviNames)
            {
                var canvas = GameObject.Find(objectName)?.GetComponent<Canvas>();
                if (canvas != null) canvas.enabled = visible;
            }
        }
    }
}