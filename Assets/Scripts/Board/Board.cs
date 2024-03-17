using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Board : MonoBehaviour {
    public static Board instance;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private float _cellSize;
    
    private Dictionary<Vector3, Tile> _tiles;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        _tiles = new Dictionary<Vector3, Tile>();
        GenerateGrid();
    }
    public Tile GetTile(Vector3 coordinates) {
        if (!_tiles.ContainsKey(coordinates)) return null;
        return _tiles[coordinates];
    }
    private void GenerateGrid() {
        for (int y = 0; y < _y; y++) {
            for (int x = 0; x < _x; x++) {
                Vector3 coordinates = new Vector3(x * _cellSize, 0f, y * _cellSize);
                Tile instantiate = Instantiate(_tilePrefab, coordinates, Quaternion.identity,transform);
                _tiles.Add(coordinates, instantiate);
                instantiate.transform.name = $"({coordinates})";
            }
        }
    }
}
