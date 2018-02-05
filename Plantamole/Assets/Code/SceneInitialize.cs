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

        /*Instantiate(player, playerStartPosition.position, Quaternion.identity);

        GameObject blockTile = Instantiate(tile);
        blockTile.GetComponent<BoxCollider2D>().isTrigger = false;
        blockTile.GetComponent<Tile>().currentSprite = null;

        GameObject tileParent = new GameObject("Tile Parent");
        
        for (int i = -6; i < 7; i++)
        {
            for (int j = -5; j < 6; j++) {
                if (i == -6 || i == 6 || j == -5 || j == 5) {
                    Instantiate(blockTile, new Vector3(i * tileWidth, j * tileWidth, 0), Quaternion.identity, tileParent.transform);
                } else {
                    GameObject newTile = Instantiate(tile, new Vector3(i * tileWidth, j * tileWidth, 0), Quaternion.identity, tileParent.transform);
                    GameController.tiles.Add(newTile.GetComponent<Tile>());
                }
            }
        }
        Destroy(blockTile);*/

        GameObject newWorm = Instantiate(worm, Vector3.zero, Quaternion.identity);
        newWorm.transform.position = GameController.GetTruePos(newWorm.transform.position);
    }
}