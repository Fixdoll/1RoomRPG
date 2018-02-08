using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    
    public Transform tileParent;
    public static List<Tile> tiles = new List<Tile>();

    int mapHeight = 9;
    int mapWidth = 11;

    private void Start() {
        for (int b=0; b < 95; b++) {
            tiles.Add(tileParent.GetChild(b).GetComponent<Tile>());
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

    public static Tile ClosestTileOnMap(Vector2 origin) {
        return ClosestTile(origin, tiles);
    }

    public static List<Tile> NeighborTiles(Tile t) {
        int index = tiles.IndexOf(t);
        List<Tile> nTiles = new List<Tile>();
        List<int> numbers = new List<int>();
        if (index < 9) {
            numbers = new List<int>(new int[] { -1, 1, 10 });
            if (index == 8) { numbers.Remove(1); }
        } else if (index >= 9 && index < 20) {
            numbers = new List<int>(new int[] { -10, -1, 1, 11});
            if (index == 9) { /*numbers.Remove(10);*/ numbers.Remove(-1); }
            else if (index == 18) { /*numbers.Remove(-9);*/ }
            else if (index == 19) { numbers.Remove(-10); /*numbers.Remove(-9);*/ numbers.Remove(1); /*numbers.Remove(12);*/ }
        } else if (index >= 20 && index < 75) {
            numbers = new List<int>(new int[] { -11, -1, 1, 11 });
            if (index == 20 || index == 31 || index == 42 || index == 53 || index == 64) { /*numbers.Remove(-12);*/ numbers.Remove(-1); /*numbers.Remove(10);*/ }
            else if (index == 30 || index == 41 || index == 52 || index == 63 || index == 74) { /*numbers.Remove(-10);*/ numbers.Remove(1); /*numbers.Remove(12);*/ }
        } else if (index >= 75 && index < 86) {
            numbers = new List<int>(new int[] { -11, -1, 1, 10 });
            if (index == 75) { /*numbers.Remove(-12);*/ numbers.Remove(-1); /*numbers.Remove(9);*/ numbers.Remove(10); }
            else if (index == 76) { /*numbers.Remove(9);*/ }
            else if (index == 85) { /*numbers.Remove(-10);*/ numbers.Remove(1); }
        } else if (index >= 86) {
            numbers = new List<int>(new int[] { -10, -1, 1 });
            if (index == 86) {
                numbers.Remove(-1);
            }
        }
        
        for (int l = 0; l < numbers.Count; l++) {
            if (index + numbers[l] >= 0 && index + numbers[l] < tiles.Count) {
                nTiles.Add(tiles[index + numbers[l]]);
            }
        }

        return nTiles;
    }

    public static Tile GetRandomTile() {
        return tiles[Random.Range(0, tiles.Count - 1)];
    }
}