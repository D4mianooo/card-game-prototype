using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitNameDisplay : MonoBehaviour {
    private TMP_Text _text;

    private void Awake() {
        _text = GetComponent<TMP_Text>();
    }
    private void UpdateText(string unitName) {
        _text.text = $"CURRENT UNIT: {unitName}";
    }
    private void OnCurrentUnitChanged(Unit unit) {
        UpdateText(unit.GetName());
    }
    private void OnEnable() {
        UnitNotifier.CurrentUnitChanged += OnCurrentUnitChanged;
    }
    private void OnDisable() {
        UnitNotifier.CurrentUnitChanged -= OnCurrentUnitChanged;
    }
}
