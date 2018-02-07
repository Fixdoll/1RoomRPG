using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Sprite unavailableSprite;
    public GameObject myHighlight;
    public GameObject content;

    void Start() {
        if (!IsAvailable()) {
            GetComponent<SpriteRenderer>().sprite = unavailableSprite;
        }
    }

    public void Highlight(bool state) {
        myHighlight.SetActive(state);
    }

    public bool IsAvailable() {
        if (content != null) {
            if (content.GetComponent<Obstacle>()) {
                return false;
            } else {
                return true;
            }
        } else {
            return true;
        }
    }

    public void SetContent(GameObject con) {
        content = con;
        if (!IsAvailable()) {
            GetComponent<SpriteRenderer>().sprite = unavailableSprite;
        }
    }
}