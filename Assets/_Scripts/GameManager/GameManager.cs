using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public event Action<GameState> OnGameStateChanged;

    private GameState _currentState;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        SetState(GameState.RedPlacement);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            SetState(_currentState == GameState.RedPlacement ? GameState.BlueAttack : GameState.RedPlacement);
        }
    }
    public GameState GetState() {
        return _currentState;
    }
    public void SetState(GameState state) {
        _currentState = state;
        
        switch (state) {
            case GameState.RedPlacement:
                break;
            case GameState.BlueAttack:
                break;
            case GameState.BluePlacement:
                break;
            case GameState.RedAttack:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        
        OnGameStateChanged?.Invoke(state);
    }
}

public enum GameState {
    RedPlacement,
    BlueAttack,
    BluePlacement,
    RedAttack
}
