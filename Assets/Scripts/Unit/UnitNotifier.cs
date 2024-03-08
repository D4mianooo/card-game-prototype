using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNotifier : MonoBehaviour {
        public static event Action<Unit> CurrentUnitChanged;
        private Unit _currentUnit;

        public Unit CurrentUnit
        {
            set
            {
                // if (_currentUnit != null) { 
                //     _currentUnit.ToggleDisplayMenu(_currentUnit);
                // }
                _currentUnit = value;
                if(CurrentUnitChanged == null) return;
                CurrentUnitChanged.Invoke(_currentUnit);
            }
        }
        private void OnMouseDown() {
            PickUnit();
        }
        private void PickUnit() {
            CurrentUnit = GetComponentInParent<Unit>();
        }
}
