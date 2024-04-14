using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {
    [SerializeField] private Card _card;
    [SerializeField] private static Action<Card> OnCardPlayed;
    private void Awake() {
        _card = GetComponentInParent<Card>();
    }
    private void OnMouseDown() {
        PlayCard();
    }
    private void PlayCard() {
        if (OnCardPlayed != null) {
            OnCardPlayed(_card);
        }
        transform.parent.rotation = Quaternion.Euler(0, 90f, 0f);
    }
}
    