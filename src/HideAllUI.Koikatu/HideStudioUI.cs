using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HideAllUI
{
    internal class HideStudioUI : HideUIAction
    {
        private string[] targets = new[] { "Canvas", "Canvas Object List", "Canvas Main Menu", "Canvas Frame Cap", "Canvas System Menu", "Canvas Guide Input", "CvsColor", "Canvas Pattern" };
        private IEnumerable<Canvas> canvasList;
        private bool visible = true;

        public HideStudioUI()
        {
            canvasList = GameObject.FindObjectsOfType<Canvas>().Where(x => targets.Contains(x.name));
        }

        public override void ToggleUI()
        {
            visible = !visible;
            foreach(var canvas in canvasList.Where(x => x))
                canvas.gameObject.SetActive(visible);

            var kkpecanvas = GameObject.Find("KKPECanvas(Clone)")?.GetComponent<Canvas>();
            if (kkpecanvas != null) kkpecanvas.enabled = visible;

            var qabcanvas = GameObject.Find("QuickAccessBoxCanvas(Clone)")?.GetComponent<Canvas>();
            if (qabcanvas != null) qabcanvas.enabled = visible;
        }
    }
}
