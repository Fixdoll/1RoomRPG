using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

    public Transform connectionPoint;

    private void Start() {
        transform.position = GameController.GetTruePos(transform.position);
        GetComponent<LineRenderer>().SetPosition(1, GameController.GetTruePos(connectionPoint.transform.position) + new Vector3(0f, 4.87f, 0f));
    }
    private void Update() {
        GetComponent<LineRenderer>().SetPosition(0, connectionPoint.transform.position);
    }
}