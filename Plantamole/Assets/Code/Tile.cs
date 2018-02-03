using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Sprite currentSprite;
    
    // Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = currentSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
