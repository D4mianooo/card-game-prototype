using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Mouse3D : MonoBehaviour {
    
    [Tooltip("Layers to ignore when computing mouse position")]
    [SerializeField] private LayerMask _ignore;

    public static Mouse3D Instance;
    private void Start() {
        Instance = this;
    }
    private void FixedUpdate() {
        transform.position = GetMouseWorldPosition();
    }
    public  Vector3 GetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,~_ignore)) return Vector3.zero;

        return hit.point;
    }
    public  Vector2 GetTileCoordinates() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, ~_ignore)) return Vector3.zero;
        Vector2 coordinates = new Vector2(hit.point.x, hit.point.y);
        coordinates = Vectors.Round(coordinates);
        return coordinates;
    }
}
