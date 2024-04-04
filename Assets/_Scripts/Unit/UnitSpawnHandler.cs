using UnityEngine;

public class UnitSpawnHandler : MonoBehaviour {
    [SerializeField] private Unit _unit;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Tile tile = BoardGenerator.Instance.GetTile(Mouse3D.Instance.GetMouseWorldPosition());
            SpawnUnit(_unit, tile);
        }
    }
    private void SpawnUnit(Unit unit, Tile tile) {
        if (tile == null) return;
        if (tile._isBusy) return;
        
        tile._isBusy = true;
        Instantiate(unit, tile.transform.position, Quaternion.identity, tile.transform);
    }
}
