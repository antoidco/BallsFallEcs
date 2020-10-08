﻿using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Game.UI {
    public class InputManager : MonoBehaviour {
        public List<UIControlComponent> UIControls;

        public void Start() {
            if (UIControls.Count > 1) {
                if (StartGame.withBot) {
                    UIControls[1].LeftButton.gameObject.SetActive(false);
                    UIControls[1].RightButton.gameObject.SetActive(false);
                }
            }
        }
    }
}