using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieObject : MonoBehaviour {

    public PlantType t;
    public Sprite inventoryIcon;

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
    }
}