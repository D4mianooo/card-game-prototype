using System;
using UnityEngine;

public class UnitSpawnHandler : MonoBehaviour {
    [SerializeField] private Unit _unit;
    
    public bool SpawnUnit(Tile tile, Unit unit) {
        if (tile == null) return false;
        if (tile._isBusy) return false;
        tile._isBusy = true;
        Vector3 spawn = ComputeSpawnPosition(tile);
        Instantiate(unit, spawn, Quaternion.identity);
        
        return true;
    }
    private Vector3 ComputeSpawnPosition(Tile tile) {
        Vector3 spawn = tile.transform.position;
        spawn.y = 0.25f;
        
        return spawn;
    }
}
