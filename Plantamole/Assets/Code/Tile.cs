using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public GameObject myHighlight;

    public void Highlight(bool state) {
        myHighlight.SetActive(state);
    }
}