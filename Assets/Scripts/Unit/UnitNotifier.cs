using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNotifier : MonoBehaviour {
        
        private Unit _selectedUnit; 
        public static event Action<Unit> SelectedUnitChanged;

        public Unit SelectedUnit
        {
            set {
                _selectedUnit = value;
                if(SelectedUnitChanged == null) return;
                OnSelectedUnitChanged();
            }
        }

        private void OnMouseDown() {
            SelectUnit();
        }
        private void SelectUnit() {
            SelectedUnit = GetComponentInParent<Unit>();
        }
        private void OnSelectedUnitChanged() {
            if (SelectedUnitChanged == null) return;
            SelectedUnitChanged.Invoke(_selectedUnit);
            
        }

}
