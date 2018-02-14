using System;
using UnityEngine;

public enum ObjectType { Unidentified = -1, Stone }

public class OtherObject : MonoBehaviour, IGroundObject {

    public Other other;
    public ObjectType t;
    public Sprite inventoryIcon;

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
    }

    public Type GetObjectType() {
        return other.GetType();
    }

    public void CreateObject() {
        other = new Other(t, inventoryIcon);
    }
}