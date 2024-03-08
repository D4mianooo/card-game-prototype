using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
        [SerializeField] private string unitName = "Rincewind";

        public void Update() {
                Debug.Log("DUPA LIPA");
        }
        public string GetName() {
                return unitName;
        }
        public void ToggleDisplayMenu(Unit unit) {
                UnitMenuDisplay unitMenuDisplay = GetComponentInChildren<UnitMenuDisplay>(true);
                unitMenuDisplay.gameObject.SetActive(!unitMenuDisplay.gameObject.activeSelf);
        }
        
        private void OnEnable() {
                UnitNotifier.CurrentUnitChanged += ToggleDisplayMenu;
        }

}
