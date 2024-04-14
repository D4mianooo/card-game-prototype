using UnityEngine;

public class TileColorHandler : MonoBehaviour {
    private Color _defaultColor;
    private Color _hoverColor;

    private MeshRenderer _meshRenderer;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
        _hoverColor = Color.green;
    }

    private void OnMouseExit() {    
        _meshRenderer.material.color = _defaultColor;
    }
    private void OnMouseOver() {
        _meshRenderer.material.color = _hoverColor;
    }
}
