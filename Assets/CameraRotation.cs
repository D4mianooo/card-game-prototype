using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRotation : MonoBehaviour {
    [SerializeField] private Transform _redAttack;
    [SerializeField] private Transform _redPlacement;
    [SerializeField] private Transform _blueAttack;
    [SerializeField] private Transform _bluePlacement;
    
    private void OnEnable() {
        GameManager.Instance.OnGameStateChanged += HandleCamera;
    }
    private void OnDisable() {
        GameManager.Instance.OnGameStateChanged -= HandleCamera;
    }
    private void HandleCamera(GameState state) {
        switch (state) {
            case GameState.RedAttack:
                RotateCamera(_redAttack);
                break;
            case GameState.RedPlacement:
                RotateCamera(_redPlacement);
                break;
            case GameState.BlueAttack:
                RotateCamera(_blueAttack);
                break;
            case GameState.BluePlacement:
                RotateCamera(_bluePlacement);
                break;
        }
    }
    private void RotateCamera(Transform target) {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}

