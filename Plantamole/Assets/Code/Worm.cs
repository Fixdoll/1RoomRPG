using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WormMode { Idle, Moving, Biting, Death }

public class Worm : Creature {

    Rigidbody2D rd;
    WormMode mode = WormMode.Idle;
    Vector2 targetPos;
    bool movingToTarget;

    void Start() {
        rd = GetComponent<Rigidbody2D>();
        targetPos = start.transform.position;
        StartCoroutine(GoToTarget(targetPos));
        movingToTarget = true;
    }

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
        List<Tile> path = FindPath(GameController.tiles[20]);
        for (int i = 0; i < path.Count-1; i++) {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position);
        }

        if (movingToTarget) {
            if (GameController.ManhattanDistance((Vector2)transform.position, targetPos) < 0.2f) {
                Debug.Log("Worm reached target.");
                rd.velocity = new Vector2(0, 0);
                StopAllCoroutines();
                // GIVE ANOTHER TARGET
                // START GOTOTARGET AGAIN
                movingToTarget = false;
            }
        }
    }

    IEnumerator GoToTarget(Vector2 targetPosition) {
        rd.AddForce((targetPosition - (Vector2)transform.position).normalized * 0.5f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.8f);
        rd.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(GoToTarget(targetPosition));
    }
}