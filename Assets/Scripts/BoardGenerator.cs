using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class BoardGenerator : MonoBehaviour {
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2Int _offset;
    [SerializeField] private float _scale;
    private bool _activeCoordinates;
    public void GenerateGrid() {
        ClearGrid();
        for (int y = 0; y < _size.x; y++) {
            for (int x = 0; x < _size.y; x++) {
                Vector3 coordinates = new Vector3((x + _offset.x) * _scale, 0f, (y + _offset.y) * _scale);
                GameObject tile = Instantiate(_tilePrefab, coordinates, Quaternion.identity, transform);
                tile.transform.localScale = new Vector3(_scale, tile.transform.localScale.y, _scale);
                tile.GetComponent<Tile>().UpdateCoordinatesLabel();
                tile.GetComponent<Tile>().ToggleCoordinates(false);
            }
        }
        
    }
    public void ClearGrid() {
        List<GameObject> tiles = new List<GameObject>();
        foreach (Transform child in transform) {
                tiles.Add(child.gameObject);
        }
        tiles.ForEach(tile =>
        {
            DestroyImmediate(tile);
        });
    }
    private void Awake() {
        GenerateGrid();
    }
}
