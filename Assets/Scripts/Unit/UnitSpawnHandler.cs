using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class UnitSpawnHandler : MonoBehaviour {
     [SerializeField] private GameObject _unitPrefab;
     [SerializeField] private Tile _tile;
     private void Awake() {
         _tile = GetComponentInParent<Tile>();
     }
     private void OnMouseOver() {
         if (Input.GetMouseButtonDown(0)) {
             if (_tile.IsOccupied()) return;
             // throwEvent;
             SpawnUnit();
             
         }
     }
     private void SpawnUnit() {
         Vector3 mouse = Mouse3D.GetMouseWorldPosition();
         Vector3 spawn = Vectors.Round(mouse);
         spawn.y = 0f;
         GameObject unit = Instantiate(_unitPrefab, spawn, Quaternion.identity);
         _tile.AlignUnit(unit);

     }
}
