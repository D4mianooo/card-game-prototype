    using System;
    using TMPro;
    using UnityEngine;

    public class GameStateDisplay : MonoBehaviour {
        [SerializeField] private TMP_Text _text;

        private void OnEnable() {
            GameManager.Instance.OnGameStateChanged += UpdateText;
        }
        private void OnDisable() {
            GameManager.Instance.OnGameStateChanged -= UpdateText;
        }
        private void UpdateText(GameState state) {
            _text.text = $"STATE: {state.ToString()}";
        }
    }
