using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {
    private Vector3 original;
    private void Start() {
        original = transform.position;
    }
    private void OnMouseDown() {
        transform.position = original + Vector3.up;
    }           
    private void OnMouseDrag() {
        transform.position = Mouse3D.Instance.GetMouseWorldPosition() + Vector3.up;
    }
    private void OnMouseUp() {
        transform.position = original;
    }
}
