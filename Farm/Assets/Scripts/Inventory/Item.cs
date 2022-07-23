using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Default, Food, Weapon, Instrument }
public class Item : ScriptableObject
{
    [SerializeField] ItemType type;
    [SerializeField] string Name;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] int maxAmount;
    [SerializeField] string aboutItem;
    [SerializeField] Sprite icon;
}
