using System;
using UnityEngine;
using Utils;

public class UnitContoller : MonoBehaviour {
    private Tile _currentTile;
    
    private void OnMouseUp() {
        Vector2 coordinates = Mouse3D.Instance.GetTileCoordinates();
        Tile tile = Board.Instance.GetTile(coordinates);
        
        if(tile == null) return;
        if(tile._isBusy) {
            return;
        }
            
        tile._isBusy = true;
        transform.parent.position = coordinates;
    }
    private void OnMouseDrag() {
        FollowMouse3D();
    }
    private void FollowMouse3D() {
        transform.parent.position = Mouse3D.Instance.GetMouseWorldPosition();
    }
}
    
