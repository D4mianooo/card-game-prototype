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
    private void OnEnable() {
        UnitNotifier.SelectedUnitChanged += UpdateText;
    }
    private void OnDisable() {
        UnitNotifier.SelectedUnitChanged -= UpdateText;
    }
    private void UpdateText(Unit unit) {
        _text.text = $"CURRENT UNIT: {unit.GetName()}";
    }
}
