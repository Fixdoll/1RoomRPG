using System;
using UnityEngine;

public enum WeaponType { Unidentified = -1, Carrot }

public class WeaponObject : MonoBehaviour, IGroundObject {

    public Weapon weapon;
    public WeaponType t;
    public Sprite inventoryIcon;
    public int bullets;

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
    }

    public Type GetObjectType() {
        return weapon.GetType();
    }

    public void CreateObject() {
        weapon = new Weapon(t, inventoryIcon, bullets);
    }
}