using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class UnitSpawnHandler : MonoBehaviour {
    [SerializeField] private Unit _unit; 
     private void Update() {
         if(Input.GetMouseButtonDown(0)) {
             Vector2 coordinates = Mouse3D.Instance.GetTileCoordinates();
             Tile tile = Board.Instance.GetTile(coordinates);
             
             if(tile == null) return;
             if(tile._isBusy) return;
             
             SpawnUnit(_unit, coordinates);
             tile._isBusy = true;
         }
     }
     private void SpawnUnit(Unit unit, Vector2 coordinates) {
         Vector3 position = Utils.Board.CoordinateTo3DPosition(coordinates);
         Instantiate(unit, position, Quaternion.identity);
     }
}
