using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotation : MonoBehaviour {
    [SerializeField] private Transform target;

    private void RotateBoard(GameState state) {
        transform.RotateAround(target.position, Vector3.up, 180f);

    }
}
