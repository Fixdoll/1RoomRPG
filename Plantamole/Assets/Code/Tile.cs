using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool edgeBlock = false;
    
	void Start () {
        
        if (edgeBlock) {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
	}
}