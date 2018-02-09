using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Obstacle {

    void Start() {
        transform.position = GameController.GetTruePos(transform.position);
    }
}