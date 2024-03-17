using System;
using UnityEngine;
using Utils;

public class UnitContoller : MonoBehaviour {
    private LayerMask _layerToIgnore;
    private Tile _currentTile;
    
    private void Start() {
        _layerToIgnore = LayerMask.GetMask("Unit","Ignore Raycast");
    }
    private void OnEnable() {
    }
    private void OnMouseDrag() {
        FollowMouse3D();
    }
    private void OnMouseUp() {
        Vector3 position = Mouse3D.GetMouseWorldPosition(~_layerToIgnore);
        position = Vectors.Round(position);
        position.y = 0f;
        Tile tile = Board.instance.GetTile(position);
        
        if(tile == null) return;
        if (tile._isBusy) {
            return;
        }
            
        tile._isBusy = true;
        transform.parent.position = position;
    }
    private void FollowMouse3D() {
        transform.parent.position = Mouse3D.GetMouseWorldPosition(~_layerToIgnore);
    }
}
    
