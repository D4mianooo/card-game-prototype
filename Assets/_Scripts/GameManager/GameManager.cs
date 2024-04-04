using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public event Action<GameState> OnGameStateChanged;

    private List<GameState> _rounds;
    private int roundNumber = 0;

    private GameState _currentState;
    private void Awake() {
        DontDestroyOnLoad(this);
        Instance = this;
        _rounds = new List<GameState>();
        InitializeRoundsList();
    }
    private void Start() {
        NextRound();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
          NextRound();
        }
    }
    private void InitializeRoundsList() {
        foreach (GameState state in (GameState[]) Enum.GetValues(typeof(GameState))) {
            _rounds.Add(state);
        }
    }
    private void NextRound() {
        GameState state = _rounds[roundNumber%4];
        roundNumber++;
        SetState(state);
    }
    public GameState GetState() {
        return _currentState;
    }
    public void SetState(GameState state) {
        _currentState = state;
        OnGameStateChanged?.Invoke(state);
    }
}

public enum GameState {
    RedPlacement,
    BlueAttack,
    BluePlacement,
    RedAttack
}
