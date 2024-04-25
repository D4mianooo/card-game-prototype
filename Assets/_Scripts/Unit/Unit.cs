using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour {
    private Tile _placedOn;
    
    public void Move(Tile _target) {
        
    }
    public void SetTile(Tile tile) {
        _placedOn = tile;
    }
}
