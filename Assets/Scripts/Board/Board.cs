using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public static Board Instance;
    
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private float _cellSize;
    
    private Dictionary<Vector2, Tile> _tiles;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        _tiles = new Dictionary<Vector2, Tile>();
        GenerateGrid();
    }
    public Tile GetTile(Vector2 coordinates) {
        if (!_tiles.ContainsKey(coordinates)) return null;
        return _tiles[coordinates];
    }
    private void GenerateGrid() {
        for (int y = 0; y < _y; y++) {
            for (int x = 0; x < _x; x++) {
                Vector3 coordinates3D = new Vector3(x * _cellSize, 0f, y * _cellSize);
                Vector2 coordinates = new Vector2(coordinates3D.x, coordinates3D.z);
                Tile instantiate = Instantiate(_tilePrefab, coordinates3D, Quaternion.identity,transform);
                _tiles.Add(coordinates, instantiate);
            }
        }
    }
}
