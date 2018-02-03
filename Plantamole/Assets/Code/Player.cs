using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class Player : MonoBehaviour {

    public Sprite currentSprite;
    public int Speed = 1;
    Rigidbody2D rd;

    void Start () {
        rd = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
	
	
	void Update () {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rd.velocity = new Vector2(rd.velocity.x, -Speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rd.velocity = new Vector2(rd.velocity.x, Speed);
        }
        else
        {
            rd.velocity = new Vector2(rd.velocity.x, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rd.velocity = new Vector2(-Speed, rd.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rd.velocity = new Vector2(Speed, rd.velocity.y);
        }
        else
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        

        /*float xPress = Input.GetAxis("Horizontal") * Speed / 100;
        float yPress = Input.GetAxis("Vertical") * Speed / 100;
        rd.MovePosition((Vector2)transform.position + new Vector2(xPress, yPress));*/


    }
}
