using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleFeetSoundController : MonoBehaviour {

    public Transform foot1;
    public Transform foot2;

    public void spawnFootSound1() {
        int randomInt = Random.Range(0, 5);
        string soundName = "MoleStepSound" + randomInt.ToString();
        Instantiate(Resources.Load(soundName), foot1.position, Quaternion.identity, GameController.game);
    }

    public void spawnFootSound2() {
        int randomInt = Random.Range(0, 5);
        string soundName = "MoleStepSound" + randomInt.ToString();
        Instantiate(Resources.Load(soundName), foot2.position, Quaternion.identity, GameController.game);
    }
}