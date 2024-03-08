using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitPlacer : MonoBehaviour {
     [SerializeField] private GameObject _unitPrefab;
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        Tile tile = hit.transform.GetComponentInParent<Tile>();


        if (tile == null) return;
        if (Input.GetMouseButtonDown(0)) {
            tile.AssignUnit(_unitPrefab);
        }
    }
}
