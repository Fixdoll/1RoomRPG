using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableObject : MonoBehaviour {

    public TextMesh highlightText;

    public void Highlight (bool state) {
        highlightText.gameObject.SetActive(state);
    }
}