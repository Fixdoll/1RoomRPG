using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Sprite currentSprite;
    public bool Selected = false;
    public GameObject myHighlight;
    
	void Start () {
        GetComponent<SpriteRenderer>().sprite = currentSprite;
	}

    void Update()
    {
        if (Selected)
            myHighlight.SetActive(true);
        else
            myHighlight.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !GameController.HighlightPresent())
            Selected = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !Selected && !GameController.HighlightPresent())
            Selected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Selected = false;
    }

}