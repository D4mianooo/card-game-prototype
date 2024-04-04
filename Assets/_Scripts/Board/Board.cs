using UnityEngine;
using System.Collections.Generic;
public class Board {
    public Board(int x, int y, Tile tile) {
        _x = x;
        _y = y;
        _tile = tile;
    }
    private int _x;
    private int _y;
    private Tile _tile;
    public Tile Tile => _tile;
    public List<Vector3> Create(int xSize, int ySize, int cellSize) {
        List<Vector3> tiles = new List<Vector3>();
        for (int y = _y; y < _y + ySize; y++) {
            for (int x = _x; x < _x + xSize; x++) {
                Vector3 position = new(x * cellSize, 0f, y * cellSize);
                tiles.Add(position);
            }
        }
        return tiles;
    }
}
