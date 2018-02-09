using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour {

    public GameObject player;
    public Transform playerStartPosition;
    public GameObject tile;
    public GameObject worm;
    public float tileWidth = 0.5f;

    void Start () {

        GameObject p = Instantiate(player, playerStartPosition.position, Quaternion.identity, transform.parent);
        p.transform.position = GameController.GetTruePos(p.transform.position);

        GameController.Plant(PlantType.Carrot, GameController.tiles[13]);
        GameController.Plant(PlantType.Carrot, GameController.tiles[14]);
        GameController.Plant(PlantType.Carrot, GameController.tiles[15]);

        GameController.SpawnCreature(CreatureType.Worm, 0);
    }
}