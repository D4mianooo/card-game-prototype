using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour {
        [SerializeField] private UnitScriptableObject _scriptableObject;
  
        public string GetName() {
                return _scriptableObject.name;
        }

                        
}
