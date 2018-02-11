using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int speed = 300;

    public GameObject face;
    public GameObject hands;
    public GameObject feet;
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject nose;
    public GameObject cheek;

    Item[] inventory = new Item[6];
    Item currentItem;
    int _currentID = 0;
    public int CurrentID
    {
        get {
            return _currentID;
        } set {
            _currentID = value;
            currentItem = inventory[_currentID];
        }
    }
    public Image[] inventoryIcons;

    // TEMP PUBLIC ICON
    public Sprite carrotSeedIcon;

    Rigidbody2D rd;
    int lookDir = 0;

    List<Tile> nearbyTiles = new List<Tile>();
    List<Tile> nearbyEmptyTiles = new List<Tile>();
    Tile highlightedTile;

    void Start () {
        rd = GetComponent<Rigidbody2D>();
        transform.position = GameController.GetTruePos(transform.position);
        CurrentID = 0;

        foreach (Tile t in GameController.tiles) {
            Physics2D.IgnoreCollision(t.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        GameController.UpdateInventoryUI(inventory);
    }
	
	void Update () {

        // MOVEMENT
        Vector2 movement = new Vector2(0f,0f);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rd.AddForce(Vector2.ClampMagnitude((movement * speed * Time.deltaTime), 5f));
            transform.position = GameController.GetTruePos(transform.position);
            feet.GetComponent<Animator>().SetBool("walking", true);
        } else {
            feet.GetComponent<Animator>().SetBool("walking", false);
        }

        // TEMP PICKUP CARROT SEED WITH SPACE
        if (Input.GetKeyDown(KeyCode.Space)) {
            Pickup(new Seed(PlantType.Carrot, carrotSeedIcon));
        }

        // FACING
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        int dirAngle = Mathf.FloorToInt(Vector2.SignedAngle(playerPos - mousePos, playerPos - (playerPos + Vector2.up)));

        if (dirAngle < -30 && dirAngle >= -90) {
            UpdateFace(0);
        } else if (dirAngle < 30 && dirAngle >= -30) {
            UpdateFace(1);
        } else if (dirAngle < 90 && dirAngle >= 30) {
            UpdateFace(2);
        } else if (dirAngle < 150 && dirAngle >= 90) {
            UpdateFace(3);
        } else if (dirAngle < -150 || dirAngle >= 150) {
            UpdateFace(4);
        } else if (dirAngle < -90 && dirAngle >= -150) {
            UpdateFace(5);
        }

        foreach (Tile t in nearbyTiles) {
            t.Highlight(false);
        }

        // HIGHLIGHTING CLOSEST GROUND OBJECT
        foreach (GameObject go in GameController.groundObjects) {
            if (go.GetComponent<HighlightableObject>()) {
                go.GetComponent<HighlightableObject>().Highlight(false);
            }
        }

        GameObject highlightedObject = GameController.ClosestGroundObjectInRange(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(mousePos), 1.2f);
        if (highlightedObject != null) {
            if (highlightedObject.GetComponent<HighlightableObject>()) {
                highlightedObject.GetComponent<HighlightableObject>().Highlight(true);
            }
        }

        // MODES
        if (currentItem == null) {
            // carrying nothing

        } else if (currentItem is Seed) {
            // carrying seed
            highlightedTile = GameController.ClosestTile((Vector2)Camera.main.ScreenToWorldPoint(mousePos), nearbyEmptyTiles);
            if (highlightedTile != null) {
                highlightedTile.Highlight(true);
                // plant on left mouse click
                if (Input.GetButtonDown("Fire1")) {
                    GameController.Plant((currentItem as Seed).t, highlightedTile);
                    RemoveCurrentItem();
                    nearbyEmptyTiles.Remove(highlightedTile);
                    highlightedTile.Highlight(false);
                }
            }
        } else {

        }
    }

    public void RemoveCurrentItem() {
        inventory[CurrentID] = null;
        CurrentID = CurrentID;
        GameController.UpdateInventoryUI(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Tile>()) {
            Tile colT = collision.GetComponent<Tile>();
            nearbyTiles.Add(colT);
            if (colT.content == null) {
                nearbyEmptyTiles.Add(colT);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Tile colT = collision.GetComponent<Tile>();
        if (colT != null) {
            nearbyTiles.Remove(colT);
            nearbyEmptyTiles.Remove(colT);
            colT.Highlight(false);
        }
    }

    void UpdateFace(int lookDir) {
        // Looking up
        if (lookDir < 3) {
            face.SetActive(false);
            hands.transform.localPosition = new Vector3(0f, 0.1f, 0.4f);
            feet.transform.localPosition = new Vector3(0f, 0.1f, 0.3f);
        } else {
            face.SetActive(true);
            hands.transform.localPosition = new Vector3(0f, 0f, -0.4f);
            feet.transform.localPosition = new Vector3(0f, 0f, -0.3f);
        }

        // Looking down
        if (lookDir == 4) {
            cheek.SetActive(false);
            leftEye.transform.localPosition = new Vector3(0.136f, leftEye.transform.localPosition.y, leftEye.transform.localPosition.z);
            rightEye.transform.localPosition = new Vector3(-0.136f, rightEye.transform.localPosition.y, rightEye.transform.localPosition.z);
            nose.transform.localPosition = new Vector3(0f, nose.transform.localPosition.y, nose.transform.localPosition.z);
            feet.transform.localPosition = new Vector3(0.094f, feet.transform.localPosition.y, feet.transform.localPosition.z);
        } else {
            cheek.SetActive(true);
            leftEye.transform.localPosition = new Vector3(-0.009f, leftEye.transform.localPosition.y, leftEye.transform.localPosition.z);
            rightEye.transform.localPosition = new Vector3(-0.23f, rightEye.transform.localPosition.y, rightEye.transform.localPosition.z);
            nose.transform.localPosition = new Vector3(-0.193f, nose.transform.localPosition.y, nose.transform.localPosition.z);
            feet.transform.localPosition = new Vector3(0f, feet.transform.localPosition.y, feet.transform.localPosition.z);
        }

        // Looking up or down
        if (lookDir == 1 || lookDir == 4) {
            hands.transform.localPosition = new Vector3(0.17f, hands.transform.localPosition.y, hands.transform.localPosition.z);
        } else {
            hands.transform.localPosition = new Vector3(0f, hands.transform.localPosition.y, hands.transform.localPosition.z);
        }

        // Looking right
        if (lookDir == 2 || lookDir == 3) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Pickup(Item item) {

        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[i] == null) {
                inventory[i] = item;
                CurrentID = CurrentID;
                Debug.Log("Player picked up a " + item + "!");
                GameController.UpdateInventoryUI(inventory);
                // DO OTHER STUFF RELATED TO PICKING UP AN ITEM
                return;
            }
        }
        // DO STUFF RELATED TO TRYING TO PICKUP WITH FULL INVENTORY
    }
}