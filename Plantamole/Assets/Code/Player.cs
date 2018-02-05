using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int speed = 300;

    public GameObject face;
    public GameObject hands;
    public GameObject feet;
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject nose;
    public GameObject cheek;

    Rigidbody2D rd;
    int lookDir = 0;

    void Start () {
        rd = GetComponent<Rigidbody2D>();
        transform.position = GameController.GetTruePos(transform.position);
    }
	
	void Update () {

        Vector2 movement = new Vector2(0f,0f);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rd.AddForce(Vector2.ClampMagnitude((movement * speed * Time.deltaTime), 5f));
            transform.position = GameController.GetTruePos(transform.position);
            feet.GetComponent<Animator>().SetBool("walking", true);
        } else {
            feet.GetComponent<Animator>().SetBool("walking", false);
        }

        Vector2 mousePos = Input.mousePosition;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        int dirAngle = Mathf.FloorToInt(Vector2.SignedAngle(playerPos - mousePos, playerPos - (playerPos + Vector2.up)));

        if (dirAngle < -30 && dirAngle >= -90) {
            UpdateFace(0);
        } else if (dirAngle < 30 && dirAngle >= -30) {
            UpdateFace(1);
        } else if (dirAngle < 90 && dirAngle >= 30) {
            UpdateFace(2);
        } else if (dirAngle < 150 && dirAngle >= 90) {
            UpdateFace(3);
        } else if (dirAngle < -150 || dirAngle >= 150) {
            UpdateFace(4);
        } else if (dirAngle < -90 && dirAngle >= -150) {
            UpdateFace(5);
        }
    }

    void UpdateFace(int lookDir) {
        // Looking up
        if (lookDir < 3) {
            face.SetActive(false);
            hands.transform.localPosition = new Vector3(0f, 0.1f, 0.4f);
            feet.transform.localPosition = new Vector3(0f, 0.1f, 0.3f);
        } else {
            face.SetActive(true);
            hands.transform.localPosition = new Vector3(0f, 0f, -0.4f);
            feet.transform.localPosition = new Vector3(0f, 0f, -0.3f);
        }

        // Looking down
        if (lookDir == 4) {
            cheek.SetActive(false);
            leftEye.transform.localPosition = new Vector3(0.136f, leftEye.transform.localPosition.y, leftEye.transform.localPosition.z);
            rightEye.transform.localPosition = new Vector3(-0.136f, rightEye.transform.localPosition.y, rightEye.transform.localPosition.z);
            nose.transform.localPosition = new Vector3(0f, nose.transform.localPosition.y, nose.transform.localPosition.z);
            feet.transform.localPosition = new Vector3(0.094f, feet.transform.localPosition.y, feet.transform.localPosition.z);
        } else {
            cheek.SetActive(true);
            leftEye.transform.localPosition = new Vector3(-0.009f, leftEye.transform.localPosition.y, leftEye.transform.localPosition.z);
            rightEye.transform.localPosition = new Vector3(-0.23f, rightEye.transform.localPosition.y, rightEye.transform.localPosition.z);
            nose.transform.localPosition = new Vector3(-0.193f, nose.transform.localPosition.y, nose.transform.localPosition.z);
            feet.transform.localPosition = new Vector3(0f, feet.transform.localPosition.y, feet.transform.localPosition.z);
        }

        // Looking up or down
        if (lookDir == 1 || lookDir == 4) {
            hands.transform.localPosition = new Vector3(0.17f, hands.transform.localPosition.y, hands.transform.localPosition.z);
        } else {
            hands.transform.localPosition = new Vector3(0f, hands.transform.localPosition.y, hands.transform.localPosition.z);
        }

        // Looking right
        if (lookDir == 2 || lookDir == 3) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
}