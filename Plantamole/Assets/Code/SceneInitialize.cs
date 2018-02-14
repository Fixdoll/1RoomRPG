using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour {

    public GameObject player;
    public Transform playerStartPosition;
    public GameObject tile;
    public GameObject worm;
    public float tileWidth = 0.5f;
    public GameObject carrotSeed;

    void Start () {

        GameObject p = Instantiate(player, playerStartPosition.position, Quaternion.identity, GameController.game);
        p.transform.position = GameController.GetTruePos(p.transform.position);

        GameController.SpawnCreature(CreatureType.Worm, 0);

        for (int l = 0; l < 4; l++) {
            GameController.AddGroundObject(Instantiate(GameController.objects[1],
                                            (Vector2)GameController.tiles[10].transform.position + new Vector2(Random.Range(-0.01f, 0.01f),
                                            Random.Range(-0.01f, 0.01f)),
                                            Quaternion.identity,
                                            GameController.game));
        }
    }
}