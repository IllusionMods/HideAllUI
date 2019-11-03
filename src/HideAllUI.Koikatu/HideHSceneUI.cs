﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KeelPlugins
{
    internal class HideHSceneUI : HideUIAction
    {
        private IEnumerable<Canvas> canvasList;
        private bool visible = true;

        public HideHSceneUI()
        {
            canvasList = GameObject.FindObjectsOfType<Canvas>().Where(x => x.name == "Canvas");
        }

        public override void ToggleUI()
        {
            visible = !visible;
            foreach(var canvas in canvasList.Where(x => x))
                canvas.enabled = visible;
        }
    }
}
