using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

	public void FindPath() {
        Tile startTile = GameController.ClosestTileOnMap(transform.position);
        Debug.DrawLine((Vector2)transform.position, (Vector2)startTile.transform.position, Color.red, 0.01f, false);
        List<Tile> neighborTiles = GameController.NeighborTiles(startTile);
        foreach (Tile t in neighborTiles) {
            Debug.DrawLine((Vector2)startTile.transform.position, (Vector2)t.transform.position, Color.blue, 0.01f, false);
        }
    }
}
