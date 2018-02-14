using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CreatureType { Unidentified = -1, Worm, Spider }

public class Creature : MonoBehaviour {

    public Tile start;
    public int health;
    public int healthMax;
    public List<int> loot; // refers to GameController.objects index
    public int lootAmount = 2;

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
            trialDistances.Add(GameController.ManhattanDistance(targetPos, t.transform.position));
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

    public void GetDamage(int dmg) {
        health = Mathf.Clamp(health-dmg, 0, healthMax);
        
        if (health == 0) {
            Die();
        }
        Color initial = GetComponentInChildren<SpriteRenderer>().color;
        GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0.05f).OnComplete(() => GetComponentInChildren<SpriteRenderer>().DOColor(initial, 0.05f));
    }

    public void Die() {
        for (int k=0; k < lootAmount; k++) {
            GameObject l00t = Instantiate(GameController.objects[loot[Random.Range(0, loot.Count)]],
                                (Vector2)transform.position,
                                Quaternion.identity,
                                GameController.game);
            GameController.AddGroundObject(l00t);
            if (l00t.GetComponent<Rigidbody2D>()) {
                l00t.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 10f);
            }
        }
        Destroy(gameObject);
    }
}