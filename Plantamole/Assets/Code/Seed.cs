using UnityEngine;

public class Seed : Item {

    public PlantType type;

    public Seed(PlantType type, Sprite icon) {
        this.type = type;
        inventoryIcon = icon;
    }
}