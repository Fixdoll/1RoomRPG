using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Sprite currentSprite;
    
	void Start () {
        GetComponent<SpriteRenderer>().sprite = currentSprite;
	}
}