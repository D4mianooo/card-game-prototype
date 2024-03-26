using UnityEngine;

public class UnitHealth : MonoBehaviour {
    [SerializeField] private Slider _slider;
    [SerializeField] private float _maxHealth = 20f;
    [SerializeField] private float _currentHealth;
    private void Start() {
        _currentHealth = _maxHealth;
    }
    public void DecreaseHealth(float Damage) {
        if (_currentHealth <= 0) return;
        _slider.
            _currentHealth -= Damage;
    }
}
