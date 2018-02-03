using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour {

    public GameObject Tile;
    public GameObject Player;

	// Use this for initialization
	void Start () {

        GameObject blockTile = Instantiate(Tile);
        blockTile.GetComponent<BoxCollider2D>().isTrigger = false;
        blockTile.GetComponent<Tile>().currentSprite = null;

        Instantiate(Player, Vector3.zero, Quaternion.identity);

        GameObject TileParent = new GameObject("Tile Parent");
        
        for (int i = -6; i < 6; i++)
        {
            for(int j = -6; j < 6; j++)
            {
                if(i == -6 || i == 5 || j == -6 || j == 5)
                    Instantiate(blockTile, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity, TileParent.transform);
                else
                    Instantiate(Tile, new Vector3(i+0.5f, j+0.5f, 0), Quaternion.identity, TileParent.transform);
            }
        }

        Destroy(blockTile);
	}
	
}
