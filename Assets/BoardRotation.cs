using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotation : MonoBehaviour {
    [SerializeField] private Transform target;

    private void OnEnable() {
        GameManager.Instance.OnGameStateChanged += RotateBoard;
    }
    private void OnDisable() {
        GameManager.Instance.OnGameStateChanged -= RotateBoard;
    }
    private void RotateBoard(GameState state) {
        transform.RotateAround(target.position, Vector3.forward, 180f);

    }
}
