using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour {
    private void FixedUpdate() {
        transform.position = GetMouseWorldPosition();
    }
    public static Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return Vector3.zero;

        return hit.point;
    }
    public static Vector3 GetMouseWorldPosition(LayerMask _layer) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,_layer)) return Vector3.zero;


        return hit.point;
    } 
}
