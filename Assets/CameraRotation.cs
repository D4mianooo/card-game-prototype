using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRotation : MonoBehaviour {
    // [SerializeField] private Transform _redAttack;
    [SerializeField] private Transform _redPlacement;
    [SerializeField] private Transform _blueAttack;
    [SerializeField] private Transform _bluePlacement;
    
    private void OnEnable() {
        GameManager.Instance.OnGameStateChanged += MoveCamera;
    }
    private void OnDisable() {
        GameManager.Instance.OnGameStateChanged -= MoveCamera;
    }
    private void MoveCamera(GameState state) {
        switch (state) {
            case GameState.RedPlacement:
                transform.position = _redPlacement.position;
                transform.rotation = _redPlacement.rotation;
                break;
            case GameState.BlueAttack:
                transform.position = _blueAttack.position;
                transform.rotation = _blueAttack.rotation;
                break;
            case GameState.BluePlacement:
                transform.position = _bluePlacement.position;
                transform.rotation = _bluePlacement.rotation;
                break;
        }
    }
}

