using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private GameState _currentState;

    public GameState GetState() {
        return _currentState;
    }
    public void SetState(GameState state) {
        switch (state) {
            case GameState.FPlayerTurn:
                
                break;
        }
    }
}

public enum GameState {
    FPlayerTurn,
    SPlayerTurn,
}
