using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int speed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        var x = Input.GetAxis("Horizontal") * speed / 100;
        var y = Input.GetAxis("Vertical") * speed / 100;
        if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.Translate(x, y, 0);


    }
}
