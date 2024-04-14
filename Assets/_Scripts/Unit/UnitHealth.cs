using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour {
    [SerializeField] private Slider _slider;
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField] private float _currentHealth;
    private void Start() {
        _currentHealth = _maxHealth;
    }
    public void DecreaseHealth(float Damage) {
        if (_currentHealth <= 0) return;
        _currentHealth -= Damage;
        _slider.value = _currentHealth / _maxHealth;
    }
}
