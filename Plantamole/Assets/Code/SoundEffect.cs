using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

    [SerializeField]
    bool isRandom = false;

	void Start () {
        if (isRandom) {
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        }
        StartCoroutine(DestroyIn(2f));
	}

    IEnumerator DestroyIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}