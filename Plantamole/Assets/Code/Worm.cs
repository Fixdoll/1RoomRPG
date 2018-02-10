using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WormMode { Idle, Moving, Biting, Death }

public class Worm : Creature {

    Rigidbody2D rd;
    WormMode mode = WormMode.Idle;
    Vector2 targetPos;

    void Start() {
        rd = GetComponent<Rigidbody2D>();
        StartCoroutine(GoToStart(start));
    }

    void Update() {
        transform.position = GameController.GetTruePos(transform.position);
        List<Tile> path = FindPath(GameController.tiles[20]);
        for (int i = 0; i < path.Count-1; i++) {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position);
        }
    }

    IEnumerator GoToStart(Tile st) {
        rd.AddForce(((Vector2)st.transform.position - (Vector2)transform.position).normalized * 0.1f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        if (((Vector2)transform.position - (Vector2)st.transform.position).magnitude > 0.2f) {
            StartCoroutine(GoToStart(start));
        } else {
            Debug.Log("REACHED START POSITION");
            rd.velocity = new Vector2(0,0);
        }
    }
}