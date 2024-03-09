using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
        [SerializeField] private UnitScriptableObject _unitSO;

        public void Update() {
                if (Input.GetKeyDown(KeyCode.R)) {
                        UnitMenuDisplay unitMenuDisplay = GetComponentInChildren<UnitMenuDisplay>(true);
                        unitMenuDisplay.ToggleMenuCanvas(false);
                }
                
        }
        public string GetName() {
                return _unitSO.name;
        }
        public void DisplayMenu(Unit unit) {
                UnitMenuDisplay unitMenuDisplay = GetComponentInChildren<UnitMenuDisplay>();
                if (unit.GetName() == _unitSO.name) {
                        unitMenuDisplay.ToggleMenuCanvas(true);
                }
                else {
                        unitMenuDisplay.ToggleMenuCanvas(false);
                        
                }
        }       
                        
        private void OnEnable() {
                UnitNotifier.CurrentUnitChanged += DisplayMenu;
        }

}
