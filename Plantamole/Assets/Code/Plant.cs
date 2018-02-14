using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType { Unidentified = -1, Carrot, Potato, Onion, Beetroot, Ginger, Peanut }

public class Plant : MonoBehaviour {

    public PlantType type = PlantType.Unidentified;
    public Sprite[] spriteCycle;
    public float growthSpeed;
    //public float lightBoost;
    //public float waterBoost;
    public float startHealth;

    //public bool lighted = false;
    //public bool watered = false;
    
    int currentPhase = 0;
    float health;
    float maxHealth;
    float growth = 0;

    bool growthSoundPlayed = false;
	
    void Start() {
        health = maxHealth = startHealth;
        if (spriteCycle != null) {
            GetComponent<SpriteRenderer>().sprite = spriteCycle[0];
        }
    }

    void Update() {
        if (currentPhase != spriteCycle.Length - 1) {
            growth += growthSpeed;
            //if (lighted) growth += lightBoost;
            //if (watered) growth += waterBoost;
            if (growth >= 100-(growthSpeed*60) && !growthSoundPlayed) {
                Instantiate(Resources.Load("PlantGrowSound"), transform.position, Quaternion.identity, GameController.game);
                growthSoundPlayed = true;
            }
            if (growth >= 100) {
                PhaseUp();
                growth = 0;
            }
        }
    }

    void PhaseUp() {
        currentPhase++;
        health += startHealth * 0.2f;
        maxHealth += startHealth * 0.2f;
        growthSoundPlayed = false;
        // spawn particles
        GetComponent<SpriteRenderer>().sprite = spriteCycle[currentPhase]; // swap sprite
        if (currentPhase == spriteCycle.Length - 1) {
            GameController.AddGroundObject(gameObject);
        }
    }

    public void GetDamage(float damage) {
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
        // damage flash
        // play sound
        if (health == 0) Kill(); // death check
    }

    public void Kill() {
        // spawn particles
        // destroy
    }

    public void HealUp(float heal) {
        health = Mathf.Max(health + heal, maxHealth); // max hp check
        // spawn particle
    }

    public void Harvest() {
        GameController.SpawnHarvestProduct(this);
        GameController.SpawnHarvestProduct(this);
        // spawn particles
        // play sound
        Destroy(gameObject);
    }
}