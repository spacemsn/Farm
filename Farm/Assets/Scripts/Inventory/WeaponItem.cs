using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/Items/Weapon")]
public class WeaponItem : Item
{
    [SerializeField] float damage;
}
