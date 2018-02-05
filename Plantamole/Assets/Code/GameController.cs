using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static List<Tile> tiles = new List<Tile>();

    public static Vector3 GetTruePos (Vector3 pos) {
        return new Vector3(pos.x, pos.y, pos.y - 10f);
    }

    public static Tile GetRandomTile() {
        if (tiles.Count > 0) {
            return tiles[Random.Range(0, GameController.tiles.Count - 1)];
        } else {
            return null;
        }
    }
}