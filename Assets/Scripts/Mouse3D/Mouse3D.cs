using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance;
    private void Start() {
        Instance = this;
    }
    private void FixedUpdate() {
        transform.position = GetMouseWorldPosition();
    }
    public Tile GetTileOnMouseWorldPosition() {
        Vector3 position = Vectors.Round(transform.position);
        position.y = 0f;
        return Board.Instance.GetTile(position);

    }
    public  Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return Vector3.zero;

        return hit.point;
    }
    public Vector3 GetMouseWorldPosition(LayerMask _layer) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,_layer)) return Vector3.zero;

        return hit.point;
    } 
}
