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
        Vector3 spawn = ComputeSpawnPosition(tile);
        Instantiate(unit, spawn, Quaternion.identity);
    }
    private Vector3 ComputeSpawnPosition(Tile tile) {
        Vector3 spawn = tile.transform.position;
        spawn.y = 0.25f;


        return spawn;
    }
}
