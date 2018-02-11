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

    public static List<GameObject> groundObjects = new List<GameObject>();
    public static GameObject[] objects;
    public GameObject carrotSeed;
    public GameObject stone;

    public static Transform game;

    public Transform entrancesParent;
    public static List<Transform> entrances = new List<Transform>();

    int mapHeight = 9;
    int mapWidth = 11;

    public Image[] inventoryIcons;
    public static Image[] invIcons = new Image[6];

    public Image[] inventoryFrames;
    public static Image[] invFrames = new Image[6];
    public Sprite activeInventoryFrame;
    public static Sprite activeFrame;
    public Sprite inactiveInventoryFrame;
    public static Sprite inactiveFrame;

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
        objects = new GameObject[] { carrotSeed, stone };

        for (int k=0; k < inventoryIcons.Length; k++) {
            invIcons[k] = inventoryIcons[k];
        }

        for (int g=0; g < inventoryFrames.Length; g++) {
            invFrames[g] = inventoryFrames[g];
        }
        activeFrame = activeInventoryFrame;
        inactiveFrame = inactiveInventoryFrame;
    }

    public static Vector3 GetTruePos (Vector3 pos) {
        return new Vector3(pos.x, pos.y, pos.y - 10f);
    }

    public static Tile ClosestTile(Vector2 origin, List<Tile> tilesToCheck) {
        if (tilesToCheck.Count > 0) {
            Tile closestTile = tilesToCheck[0];
            for (int i = 1; i < tilesToCheck.Count; i++) {
                if (ManhattanDistance(origin, tilesToCheck[i].transform.position) < ManhattanDistance(origin, closestTile.transform.position)) {
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

    public static void AddGroundObject(GameObject go) {
        groundObjects.Add(go);
    }

    public static void RemoveGroundObject(GameObject go) {
        if (groundObjects.Contains(go)) {
            groundObjects.Remove(go);
        }
    }

    public static GameObject ClosestGroundObjectInRange(Vector2 origin, Vector2 assist, float range) {
        List<GameObject> groundObjectsInRange = new List<GameObject>();
        foreach (GameObject o in groundObjects) {
            if (o != null) {
                if (ManhattanDistance(origin, o.transform.position) < range) {
                    groundObjectsInRange.Add(o);
                }
            }
        }
        if (groundObjectsInRange.Count > 0) {
            GameObject closest = groundObjectsInRange[0];
            for (int i = 1; i < groundObjectsInRange.Count; i++) {
                if (ManhattanDistance(assist, groundObjectsInRange[i].transform.position) < ManhattanDistance(assist, closest.transform.position)) {
                    closest = groundObjectsInRange[i];
                }
            }
            return closest;
        } else {
            return null;
        }
    }

    public static void UpdateInventoryUI(Item[] inv, int currentID) {
        for (int i = 0; i < invIcons.Length; i++) {
            if (inv[i] != null) {
                invIcons[i].sprite = inv[i].inventoryIcon;
                invIcons[i].CrossFadeAlpha(1f, 0f, true);
            } else {
                invIcons[i].CrossFadeAlpha(0f, 0f, true);
            }
            invFrames[i].sprite = inactiveFrame;
        }
        invFrames[currentID].sprite = activeFrame;
    }

    public static void SpawnHarvestSeed(Plant harvestedPlant) {
        GameObject seed = objects[0];
        switch (harvestedPlant.type) {
            case PlantType.Carrot:
                seed = objects[0];
                break;
        }
        AddGroundObject(Instantiate(seed, harvestedPlant.transform.position, Quaternion.identity, game));
    }

    // USE THIS TO GET VECTOR2 DISTANCES
    public static float ManhattanDistance(Vector2 pos1, Vector2 pos2) {
        return Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y);
    }

}