using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BoardGenerator : MonoBehaviour {
    public static BoardGenerator Instance;

    [SerializeField] private Tile _redTilePrefab;
    [SerializeField] private Tile _blueTilePrefab;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private int _cellSize;

    private Dictionary<Vector3, Tile> _tiles;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        _tiles = new Dictionary<Vector3, Tile>();
        
        Board redBoard = new Board(0, 0, _redTilePrefab);
        Board blueBoard = new Board(6, 0, _blueTilePrefab);
        
        GenerateBoard(redBoard);
        GenerateBoard(blueBoard);

    }

    public Tile GetTile(Vector3 position) {
        Vector3 coordinates = ParsePositionToCoordinates(position);
        if (!_tiles.ContainsKey(coordinates)) {
            return null;
        }
        return _tiles[coordinates];
    }

    private Vector3 ParsePositionToCoordinates(Vector3 position) {
        Vector3 coordinates = new(Mathf.Round(position.x), 0f, Mathf.Round(position.z));
    
        return coordinates;
    }

    private void GenerateBoard(Board board) {
        List<Vector3> coordinates = board.Create(_x, _y, _cellSize);
        foreach (Vector3 position in coordinates) {
            Tile tile = Instantiate(board.Tile, position, Quaternion.identity, transform);
            _tiles.Add(position, tile);
        }
    }
}

