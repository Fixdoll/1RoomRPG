using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour {

    void Start() {
        StartCoroutine(DestroyIn(2f));
    }

    IEnumerator DestroyIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}