// using System;
// using UnityEngine;
//
// public class UnitContoller : MonoBehaviour {
//     private Tile _currentTile;
//     private void OnMouseDown() {
//         _currentTile = BoardGenerator.Instance.GetTile(Mouse3D.Instance.GetMouseWorldPosition());
//     }
//     private void OnMouseDrag() {
//         FollowMouse();
//     }
//     private void OnMouseUp() {
//         Tile tile = BoardGenerator.Instance.GetTile(Mouse3D.Instance.GetMouseWorldPosition());
//         if(tile.IsOccupied()){
//             transform.position = _currentTile.transform.position;
//         }
//         else{
//             MoveToTile(tile);
//         }
//
//     }
//
//     private void MoveToTile(Tile tile) {
//         if(tile == null) return;
//         
//         _currentTile.IsOccupied() = false;
//         _currentTile = tile;
//         tile.IsOccupied() = true;
//
//         transform.parent.position = tile.transform.position;
//     }
//
//     private void FollowMouse() {
//         transform.parent.position = Mouse3D.Instance.GetMouseWorldPosition();
//     }
// }
