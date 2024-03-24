using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRotation : MonoBehaviour {
    [SerializeField] private Transform _target;
    
    private void OnEnable() {
        GameManager.Instance.OnGameStateChanged += RotateBoard;
    }
    private void OnDisable() {
        GameManager.Instance.OnGameStateChanged -= RotateBoard;
    }
    private void RotateBoard(GameState state) {
        transform.RotateAround(_target.position, Vector3.up, 180f);

    }
}

