using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public Sprite unavailableSprite;
    public GameObject myHighlight;
    public Object content;

    public void Highlight(bool state) {
        myHighlight.SetActive(state);
    }

    public bool IsAvailable() {
        if (content is Obstacle) {
            return false;
        } else {
            return true;
        }
    }

    public void SetContent(Object con) {
        content = con;
        if (!IsAvailable()) {
            GetComponent<Image>().sprite = unavailableSprite;
        }
    }
}