using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class UnitSpawnHandler : MonoBehaviour {
    [SerializeField] private Unit _unit; 
     private void Update() {
         if(Input.GetMouseButtonDown(0)) {
             Vector3 spawnPosition = Vectors.Round(transform.position);
             spawnPosition.y = 0f; 
             Tile tile = Board.instance.GetTile(spawnPosition);
             
             if(tile == null) return;
             if(tile._isBusy) return;
             SpawnUnit(_unit, spawnPosition);
             tile._isBusy = true;
         }
     }
     private void SpawnUnit(Unit unit, Vector3 spawnPosition) {
         Instantiate(unit, spawnPosition, Quaternion.identity);
     }
}
