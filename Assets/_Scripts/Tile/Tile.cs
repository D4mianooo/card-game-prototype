using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour {
    private Unit _occupiedBy;
    public bool IsOccupied() {
        return _occupiedBy != null;
    }
    public void SetOccupied(Unit unit) {
        _occupiedBy = unit;
    }
}
