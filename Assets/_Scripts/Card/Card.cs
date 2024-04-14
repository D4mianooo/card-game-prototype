using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    [SerializeField] private CardScriptableObject _card;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameLabel;
    private void Awake() {
        _nameLabel = GetComponentInChildren<TMP_Text>();
        _image = GetComponentInChildren<Image>();
    }
    private void Start() {
        _nameLabel.text = _card.Name;
        _image.sprite = _card.Image;
    }
    public Unit GetUnit() {
        return _card.Unit;
    }
}
