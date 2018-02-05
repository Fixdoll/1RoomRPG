using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour {

    Rigidbody2D rd;
    string mode = "idle";
    Vector2 targetPos;
    //Plant targetPlant;

	void Start () {
        rd = GetComponent<Rigidbody2D>();
        /*if (GameController.GetRandomTile() != null) {
            targetPos = GameController.GetRandomTile().transform.position;
        } else {
            targetPos = Vector2.zero;
        }
        StartCoroutine(GoTo(targetPos));*/
        //FindTargetPlant();
    }

    /*void FindTargetPlant() {
        // check if there is any plant
        // go idle if not
    }*/

    //for now picks a random tile position forever
    /*IEnumerator GoTo(Vector2 targetPos) {
        Vector2 dir = (new Vector2(transform.position.x, transform.position.y) - targetPos).normalized;
        Debug.Log(dir);
        rd.velocity = dir;
        if ((new Vector2(transform.position.x, transform.position.y) - targetPos).magnitude < 1f) {
            targetPos = GameController.GetRandomTile().transform.position;
            yield return new WaitForEndOfFrame();
            StartCoroutine(GoTo(targetPos));
        } else {
            yield return new WaitForEndOfFrame();
            StartCoroutine(GoTo(targetPos));
        }
    }*/
}