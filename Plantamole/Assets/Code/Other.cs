using UnityEngine;

public class Other : Item {

    public ObjectType type;

    public Other(ObjectType type, Sprite icon) {
        this.type = type;
        inventoryIcon = icon;
    }
}