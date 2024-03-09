using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenuDisplay : MonoBehaviour {
    private Canvas _menuCanvas;
    private void Awake() {
        _menuCanvas = GetComponent<Canvas>();
        _menuCanvas.enabled = false;
    }
    public void ToggleMenuCanvas(bool active) {
        _menuCanvas.enabled = active;
    }
}
