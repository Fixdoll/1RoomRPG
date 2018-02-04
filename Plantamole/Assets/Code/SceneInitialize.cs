using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialize : MonoBehaviour {

    public GameObject player;
    public Transform playerStartPosition;
    /*public GameObject tile;
    public float tileWidth = 0.5f;*/

    void Start () {

        Instantiate(player, playerStartPosition.position, Quaternion.identity);

        // Bu yontemle her seyi her seferinde bos yere spawn ediyoruz. Eger her oynanista degismesini istedigimiz bir sey olursa bunu prefab'in uzerinden yapabiliriz.
        // Tile'in sprite'inin random olarak belirlenmesi gibi. Editorde map'i yapmasi cok kolay, buna gerek yok yani.

        /*
        GameObject blockTile = Instantiate(tile);
        blockTile.GetComponent<BoxCollider2D>().isTrigger = false;
        blockTile.GetComponent<Tile>().currentSprite = null;

        GameObject TileParent = new GameObject("Tile Parent");
        
        for (int i = -6; i < 6; i++)
        {
            for(int j = -6; j < 6; j++)
            {
                if(i == -6 || i == 5 || j == -6 || j == 5)
                    Instantiate(blockTile, new Vector3(i*tileWidth + (tileWidth / 2), j * tileWidth + (tileWidth / 2), 0), Quaternion.identity, TileParent.transform);
                else
                    Instantiate(tile, new Vector3(i * tileWidth + (tileWidth / 2), j * tileWidth + (tileWidth / 2), 0), Quaternion.identity, TileParent.transform);
            }
        }

        Destroy(blockTile);*/
    }
	
}
