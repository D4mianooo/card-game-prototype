using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private TMP_Text _coordinatesLabel;
    [SerializeField] private GameObject _unit = null;

    public void AssignUnit(GameObject unit) {
        if (IsOccupied()) return;
        Instantiate(unit, transform);
        _unit = unit;

    }
    public void UpdateCoordinatesLabel() {
        _coordinatesLabel.text =
            $"({transform.position.x / transform.localScale.x},{transform.position.z / transform.localScale.x})";
    }
    public void ToggleCoordinates(bool enable) {
        _coordinatesLabel.enabled = enable;
    }
    private bool IsOccupied() {
        return _unit != null;
    }


}