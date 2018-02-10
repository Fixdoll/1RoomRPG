using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform tileParent;
    public static List<Tile> tiles = new List<Tile>();

    public static List<Plant> spawnedPlants = new List<Plant>();
    public static GameObject[] plants;
    public GameObject carrot;
    public GameObject potato;
    public GameObject onion;
    public GameObject beetroot;
    public GameObject ginger;

    public static List<Creature> spawnedCreatures = new List<Creature>();
    public static GameObject[] creatures;
    public GameObject worm;
    public GameObject spider;

    public static Transform game;

    public Transform entrancesParent;
    public static List<Transform> entrances = new List<Transform>();

    int mapHeight = 9;
    int mapWidth = 11;

    private void Start() {
        game = this.transform;
        for (int b=0; b < 95; b++) {
            tiles.Add(tileParent.GetChild(b).GetComponent<Tile>());
        }
        for (int g=0; g < 4; g++) {
            entrances.Add(entrancesParent.GetChild(g));
        }
        plants = new GameObject[] { carrot, potato, onion, beetroot, ginger };
        creatures = new GameObject[] { worm, spider };
    }

    public static Vector3 GetTruePos (Vector3 pos) {
        return new Vector3(pos.x, pos.y, pos.y - 10f);
    }

    public static Tile ClosestTile(Vector2 origin, List<Tile> tilesToCheck) {
        if (tilesToCheck.Count > 0) {
            Tile closestTile = tilesToCheck[0];
            for (int i = 1; i < tilesToCheck.Count; i++) {
                if (((Vector2)tilesToCheck[i].transform.position - origin).magnitude < ((Vector2)closestTile.transform.position - origin).magnitude) {
                    closestTile = tilesToCheck[i];
                }
            }
            return closestTile;
        } else {
            return null;
        }
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

    public static void Plant(PlantType type, Tile t) {
        if (t.content == null) {
            GameObject spawnedPlant = Instantiate(plants[(int)type], GetTruePos(t.transform.position), Quaternion.identity, game);
            spawnedPlants.Add(spawnedPlant.GetComponent<Plant>());
            t.content = spawnedPlant;
        }
    }

    public static void SpawnCreature(CreatureType type, int entrance) {
        int startTileIndex = 31;
        int rInt = Random.Range(0, 3);
        switch (entrance) {
            case 0:
                if (rInt == 0) startTileIndex = 31;
                else if (rInt == 1) startTileIndex = 42;
                else startTileIndex = 53;
                break;
            case 1:
                startTileIndex = 1;
                break;
            case 2:
                startTileIndex = 7;
                break;
            case 3:
                if (rInt == 0) startTileIndex = 41;
                else if (rInt == 1) startTileIndex = 52;
                else startTileIndex = 63;
                break;
        }

        GameObject spawnedCreature = Instantiate(creatures[(int)type], entrances[entrance].position, Quaternion.identity, game);
        Creature sc = spawnedCreature.GetComponent<Creature>();
        spawnedCreatures.Add(sc);
        sc.start = tiles[startTileIndex];
    }
}