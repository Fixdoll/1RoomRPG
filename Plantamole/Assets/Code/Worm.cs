using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Creature {

    Rigidbody2D rd;
    string mode = "idle";
    Vector2 targetPos;

    void LateUpdate() {
        FindPath();
    }
}