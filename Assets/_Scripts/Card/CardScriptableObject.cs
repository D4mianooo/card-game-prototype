using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObject/Card", order = 1)]
public class CardScriptableObject : ScriptableObject {
    public Sprite Image;
    public string Name;
    public Unit Unit;
}
