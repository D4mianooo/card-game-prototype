using System;
using UnityEngine;

public class UnitNotifier : MonoBehaviour {

    private Unit _selectedUnit;

    public Unit SelectedUnit
    {
        set
        {
            _selectedUnit = value;
            if (SelectedUnitChanged == null) {
                return;
            }
            OnSelectedUnitChanged();
        }
    }

    private void OnMouseDown() {
        SelectUnit();
    }
    public static event Action<Unit> SelectedUnitChanged;
    private void SelectUnit() {
        SelectedUnit = GetComponentInParent<Unit>();
    }
    private void OnSelectedUnitChanged() {
        if (SelectedUnitChanged == null) {
            return;
        }
        SelectedUnitChanged.Invoke(_selectedUnit);

    }
}
