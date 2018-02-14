using System;
using UnityEngine;

public class SeedObject : MonoBehaviour, IGroundObject {

    public Seed seed;
    public PlantType t;
    public Sprite inventoryIcon;

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
    }

    public Type GetObjectType() {
        return seed.GetType();
    }

    public void CreateObject() {
        seed = new Seed(t, inventoryIcon);
    }
}