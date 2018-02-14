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

    public Transform weaponHands;
    public GameObject carryHands;

    int dirAngle;

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
            GameController.UpdateInventoryUI(inventory, CurrentID);
        }
    }
    public Image[] inventoryIcons;

    Rigidbody2D rd;
    //int lookDir = 0;

    List<Tile> nearbyTiles = new List<Tile>();
    List<Tile> nearbyEmptyTiles = new List<Tile>();
    Tile highlightedTile;

    public enum CharStances { Empty, Weapon, Carry }

    public SpriteRenderer carriedItem;

    void Start () {
        rd = GetComponent<Rigidbody2D>();
        CurrentID = 0;

        foreach (Tile t in GameController.tiles) {
            Physics2D.IgnoreCollision(t.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
	
	void Update () {

        transform.position = GameController.GetTruePos(transform.position);

        // MOVEMENT
        Vector2 movement = new Vector2(0f,0f);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rd.AddForce(Vector2.ClampMagnitude((movement * speed * Time.deltaTime), 5f));
            feet.GetComponent<Animator>().SetBool("walking", true);
        } else {
            feet.GetComponent<Animator>().SetBool("walking", false);
        }

        // FACING
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        dirAngle = Mathf.FloorToInt(Vector2.SignedAngle(playerPos - mousePos, playerPos - (playerPos + Vector2.up)));

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

        // SCROLL INVENTORY
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if(scroll > 0) {
            if (CurrentID == 0) {
                CurrentID = 5;
            } else {
                CurrentID--;
            }
        } else if (scroll < 0) {
            if (CurrentID == 5) {
                CurrentID = 0;
            } else {
                CurrentID++;
            }
        }

        // DE-HIGHLIGHT NEARBY TILES
            foreach (Tile t in nearbyTiles) {
            t.Highlight(false);
        }

        // HIGHLIGHTING CLOSEST GROUND OBJECT

        GameObject highlightedObject = GameController.ClosestGroundObjectInRange(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(mousePos), 1.2f);
        if (highlightedObject != null) {
            GameController.ShowHighlightText(highlightedObject.GetComponent<Highlightable>().text, highlightedObject.transform.position);
        } else {
            GameController.HideHighlightText();
        }

        // INTERACT WITH E
        if (Input.GetButtonDown("Interact")) {
            if (highlightedObject != null) {
                if (highlightedObject.GetComponent<Plant>()) {
                    highlightedObject.GetComponent<Plant>().Harvest();
                } else if (highlightedObject.GetComponent<SeedObject>()) {
                    if (Pickup(highlightedObject.GetComponent<SeedObject>().seed)) {
                        Destroy(highlightedObject.gameObject);
                    }
                } else if (highlightedObject.GetComponent<WeaponObject>()) {
                    if (Pickup(highlightedObject.GetComponent<WeaponObject>().weapon)) {
                        Destroy(highlightedObject.gameObject);
                    }
                } else if (highlightedObject.GetComponent<OtherObject>()) {
                    if (Pickup(highlightedObject.GetComponent<OtherObject>().other)) {
                        Destroy(highlightedObject.gameObject);
                    }
                }
            }
        }

        // MODES
        if (currentItem == null) {
            // carrying nothing
            ChangeStance(CharStances.Empty);

        } else if (currentItem is Seed) {
            // carrying seed
            carriedItem.sprite = currentItem.inventoryIcon;
            ChangeStance(CharStances.Carry);
            highlightedTile = GameController.ClosestTile((Vector2)Camera.main.ScreenToWorldPoint(mousePos), nearbyEmptyTiles);
            if (highlightedTile != null) {
                highlightedTile.Highlight(true);
                // plant on left mouse click
                if (Input.GetButtonDown("Fire1")) {
                    GameController.Plant((currentItem as Seed).type, highlightedTile);
                    RemoveCurrentItem();
                    nearbyEmptyTiles.Remove(highlightedTile);
                    highlightedTile.Highlight(false);
                }
            }
        } else if (currentItem is Weapon) {
            // carrying weapon
            ChangeStance(CharStances.Weapon);

            for(int u=0; u < weaponHands.childCount; u++) {
                weaponHands.GetChild(u).gameObject.SetActive(false);
            }

            WeaponType currentWeaponType = (currentItem as Weapon).type;

            switch (currentWeaponType) {
                case WeaponType.Carrot:
                    weaponHands.GetChild(0).gameObject.SetActive(true);
                    break;
            }

            if (Input.GetButtonDown("Fire1")) {
                Fire(currentWeaponType);
            }
        } else if (currentItem is Other) {
            // carrying other objects
            carriedItem.sprite = currentItem.inventoryIcon;
            ChangeStance(CharStances.Carry);
        }
    }

    public void Fire (WeaponType t) {
        switch (t) {
            case WeaponType.Carrot:
                Instantiate(Resources.Load("CarrotFireSound"), transform.position, Quaternion.identity, GameController.game);
                GameObject bullet = Instantiate(GameController.bullets[0], weaponHands.position + (Input.mousePosition - Camera.main.WorldToScreenPoint(weaponHands.position)).normalized * 0.5f, Quaternion.Euler(0,0,-dirAngle+90), GameController.game);
                Physics2D.IgnoreCollision(bullet.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
                bullet.GetComponent<Rigidbody2D>().AddForce((Vector2)bullet.transform.right * 0.2f, ForceMode2D.Impulse);
                break;
        }
    }

    public void ChangeStance(CharStances to) {

        hands.SetActive(false);
        carryHands.SetActive(false);
        weaponHands.gameObject.SetActive(false);

        switch (to) {
            case CharStances.Empty:
                hands.SetActive(true);
                break;
            case CharStances.Weapon:
                weaponHands.gameObject.SetActive(true);
                break;
            case CharStances.Carry:
                carryHands.SetActive(true);
                break;
        }
    }

    public void RemoveCurrentItem() {
        inventory[CurrentID] = null;
        CurrentID = CurrentID;
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

        Vector2 mousePos = Input.mousePosition;
        Vector2 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        int weaponDir = -Mathf.FloorToInt(Vector2.SignedAngle(playerPos - mousePos, playerPos - (playerPos + Vector2.up))) - 90;

        // Looking up
        if (lookDir < 3) {
            face.SetActive(false);
            hands.transform.localPosition = new Vector3(0f, 0.1f, 0.4f);
            weaponHands.transform.localPosition = new Vector3(0f, 0.05f, 0.4f);
            carryHands.transform.localPosition = new Vector3(0f, 0.1f, 0.4f);
            feet.transform.localPosition = new Vector3(0f, 0.1f, 0.3f);
        } else {
            face.SetActive(true);
            hands.transform.localPosition = new Vector3(0f, 0f, -0.004f);
            weaponHands.transform.localPosition = new Vector3(0f, -0.05f, -0.004f);
            carryHands.transform.localPosition = new Vector3(0f, 0f, -0.004f);
            feet.transform.localPosition = new Vector3(0f, 0f, -0.003f);
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
            weaponHands.eulerAngles = new Vector3(0f, 0f, weaponDir + 180);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
            weaponHands.eulerAngles = new Vector3(0f, 0f, weaponDir);
        }
    }

    bool Pickup(Item item) {

        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[CurrentID] == null) {
                inventory[CurrentID] = item;
                CurrentID = CurrentID;
                // DO OTHER STUFF RELATED TO PICKING UP AN ITEM
                return true;
            } else if (inventory[i] == null) {
                inventory[i] = item;
                CurrentID = CurrentID;
                // DO OTHER STUFF RELATED TO PICKING UP AN ITEM
                return true;
            }
        }
        return false;
        // DO STUFF RELATED TO TRYING TO PICKUP WITH FULL INVENTORY
    }
}