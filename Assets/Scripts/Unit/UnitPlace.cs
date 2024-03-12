using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class UnitPlace : MonoBehaviour {
     [SerializeField] private GameObject _unitPrefab;
     private void OnMouseOver() {
         if (Input.GetMouseButtonDown(0)) {
             Vector3 mouse = Mouse3D.GetMouseWorldPosition(~LayerMask.GetMask("Unit"));
             Vector3 spawn = Vectors.Round(mouse);
             Instantiate(_unitPrefab, spawn, Quaternion.identity);
         }
     }
}
