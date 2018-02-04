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

        Vector2 movement = new Vector2(0f,0f);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        rd.AddForce(Vector2.ClampMagnitude((movement * Speed * Time.deltaTime), 5f));
        
        /*float xPress = Input.GetAxis("Horizontal") * Speed / 100;
        float yPress = Input.GetAxis("Vertical") * Speed / 100;
        rd.MovePosition((Vector2)transform.position + new Vector2(xPress, yPress));*/

    }
}
