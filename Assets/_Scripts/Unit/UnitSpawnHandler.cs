using System;
using UnityEngine;

public class UnitSpawnHandler : MonoBehaviour {
    [SerializeField] private Unit _unitPrefab;
    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        Vector3 mousePosition = Mouse3D.Instance.GetMouseWorldPosition();
        if(!BoardGenerator.Instance.GetTile(mousePosition)) return;
        Tile tile = BoardGenerator.Instance.GetTile(mousePosition);
        if(tile.IsOccupied()) return; 
        
        SpawnUnit(tile, _unitPrefab);
        tile.SetOccupied(_unitPrefab);
    }
    private void SpawnUnit(Tile tile, Unit unit) {
        Unit unitInstance = Instantiate(unit, tile.transform.position, Quaternion.identity);
        unitInstance.SetTile(tile);
    }
}
