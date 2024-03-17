using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour {
    [SerializeField] private GameObject _unit = null;
    
    public bool IsOccupied() {
        return _unit != null;
    }
    public void AlignUnit([CanBeNull] GameObject unit) {
        _unit = unit;
    }
    
    
    

}