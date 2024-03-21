using System.Collections.Generic;
using UnityEngine;
using Utils;

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

    public Tile GetTile(Vector3 position) {
        Vector2 coordinates = ParsePositionToCoordinates(position);
        if (!_tiles.ContainsKey(coordinates)) {
            return null;
        }


        return _tiles[coordinates];
    }

    private Vector2 ParsePositionToCoordinates(Vector3 position) {
        position = Vectors.Round(position);
        Vector2 coordinates = new(position.x, position.z);


        return coordinates;
    }

    private void GenerateGrid() {
        for (int y = 0; y < _y; y++) {
            for (int x = 0; x < _x; x++) {
                Vector3 position = new(x * _cellSize, 0f, y * _cellSize);
                Tile instantiate = Instantiate(_tilePrefab, position, Quaternion.identity, transform);
                _tiles.Add(ParsePositionToCoordinates(position), instantiate);
            }
        }
    }
}
