using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour {

    public GameObject player;
    public Transform playerStartPosition;
    public GameObject tile;
    public GameObject worm;
    public float tileWidth = 0.5f;

    void Start () {

        

        GameObject newWorm = Instantiate(worm, Vector3.zero, Quaternion.identity);
        newWorm.transform.position = GameController.GetTruePos(newWorm.transform.position);
    }
}