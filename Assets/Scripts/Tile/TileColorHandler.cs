using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColorHandler : MonoBehaviour {
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;
    private Color _hoverColor;
    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
        _hoverColor = Color.green;
    }
    private void OnMouseOver() {
        _meshRenderer.material.color = _hoverColor;
    }

    private void OnMouseExit() {
        _meshRenderer.material.color = _defaultColor;
    }
}
