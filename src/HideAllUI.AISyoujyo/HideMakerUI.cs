﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HideAllUI
{
    internal class HideMakerUI : HideUIAction
    {
        private IEnumerable<Canvas> canvasList;
        private bool visible = true;

        public override void ToggleUI()
        {
            if(canvasList == null)
            {
                var go = GameObject.Find("CustomControl");
                canvasList = go.GetComponentsInChildren<Canvas>().Where(x => x.gameObject.name.Contains("Canvas"));
            }

            visible = !visible;
            foreach(var canvas in canvasList.Where(x => x))
                canvas.enabled = visible;
        }
    }
}
