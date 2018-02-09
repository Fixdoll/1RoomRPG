using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureType { Unidentified = -1, Worm, Spider }

public class Creature : MonoBehaviour {

    public Tile start;

    public List<Tile> FindPath(Tile target) {
        List<Tile> path = new List<Tile>();
        // get start tile
        Tile startTile = GameController.ClosestTileOnMap(transform.position);
        Debug.DrawLine((Vector2)transform.position, (Vector2)startTile.transform.position, Color.red, 0.01f, false);
        PathStep(startTile, target, path);
        return path;
    }

    bool PathStep (Tile tile, Tile target, List<Tile> pathToAdd) {
        Vector2 targetPos = target.transform.position;
        // get neighboring tiles
        List<Tile> neighborTiles = GameController.NeighborTiles(tile);
        // get available tiles
        List<Tile> tilesToTry = new List<Tile>();
        foreach (Tile t in neighborTiles) {
            if (t == target) {
                pathToAdd.Add(t);
                return true;
            }
            if (t.IsAvailable()) {
                tilesToTry.Add(t);
            }
        }
        // get distances
        List<float> trialDistances = new List<float>();
        foreach (Tile t in tilesToTry) {
            trialDistances.Add((targetPos - (Vector2)t.transform.position).magnitude);
        }
        int shortestDistID = 0;
        for (int i = 1; i < trialDistances.Count; i++) {
            if (trialDistances[i] < trialDistances[shortestDistID]) {
                shortestDistID = i;
            }
        }
        pathToAdd.Add(tilesToTry[shortestDistID]);
        PathStep(tilesToTry[shortestDistID], target, pathToAdd);
        return true;
    }
}