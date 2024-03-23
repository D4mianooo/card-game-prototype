using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public event Action<GameState> OnGameStateChanged;
    
    private GameState _currentState;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        SetState(GameState.FPlayerTurn);
    }
    public GameState GetState() {
        return _currentState;
    }
    public void SetState(GameState state) {
        if (OnGameStateChanged != null) {
            OnGameStateChanged(state);
        }
        switch (state) {
            case GameState.FPlayerTurn:
                
                break;
            case GameState.SPlayerTurn:
                
                break;
        }
    }
}

public enum GameState {
    FPlayerTurn,
    SPlayerTurn,
}
