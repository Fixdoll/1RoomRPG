using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item {

    public PlantType t;

    public Seed(PlantType type, Sprite icon) {
        t = type;
        inventoryIcon = icon;
    }
}