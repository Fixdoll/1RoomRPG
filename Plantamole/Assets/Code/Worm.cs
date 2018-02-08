using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Creature {

    Rigidbody2D rd;
    string mode = "idle";
    Vector2 targetPos;

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
        List<Tile> path = FindPath(GameController.tiles[20]);
        for (int i = 0; i < path.Count-1; i++) {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position);
        }
    }
}