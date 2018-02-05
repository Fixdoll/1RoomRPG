using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{

    public List<Tile> xTiles;
    public static List<Tile> Tiles = new List<Tile>();

    private void Start()
    {
        foreach (Tile xt in xTiles)
        {
            Tiles.Add(xt);
        }
    }


    public static Vector3 GetTruePos (Vector3 pos) {
        return new Vector3(pos.x, pos.y, pos.y - 10f);
    }

    public static bool HighlightPresent()
    {
        foreach(Tile t in Tiles)
        {
            if (t.Selected) return true;
        }
        return false;
    }
}