using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public List<Tile> xTiles;
    public static Tile[][] tiles;

    int mapHeight = 9;
    int mapWidth = 11;

    private void Start() {
        tiles = new Tile[mapHeight][];

        for (int i = 0; i < mapHeight; ++i) {
            tiles[i] = new Tile[mapWidth];
        }

        int miss = 0;
        for (int k = 0; k < mapHeight; k++) {
            for (int i = 0; i < mapWidth; i++) {
                int id = k * mapWidth + i;
                if (id == 0 || id == 10 || id == 88 || id == 98) {
                    tiles[k][i] = null;
                    miss--;
                } else {
                    tiles[k][i] = xTiles[id+miss];
                }
            }
        }
    }

    public static Vector3 GetTruePos (Vector3 pos) {
        return new Vector3(pos.x, pos.y, pos.y - 10f);
    }

    public static Tile ClosestTile(Vector2 origin, List<Tile> tilesToCheck) {
        Tile closestTile = tilesToCheck[0];
        for (int i=1; i < tilesToCheck.Count; i++) {
            if (((Vector2)tilesToCheck[i].transform.position - origin).magnitude < ((Vector2)closestTile.transform.position - origin).magnitude) {
                closestTile = tilesToCheck[i];
            }
        }
        return closestTile;
    }

    /*public static bool HighlightPresent() {
        foreach (Tile t in tiles) {
            if (t.Selected) return true;
        }
        return false;
    }*/

    /*public static Tile GetRandomTile() {
        if (tiles.Count > 0) {
            return tiles[Random.Range(0, GameController.tiles.Count - 1)];
        } else {
            return null;
        }
    }*/
}