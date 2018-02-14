using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public WeaponType type;
    public int bullets;

    public Weapon(WeaponType type, Sprite icon, int bullets) {
        this.type = type;
        inventoryIcon = icon;
        this.bullets = bullets;
    }
}