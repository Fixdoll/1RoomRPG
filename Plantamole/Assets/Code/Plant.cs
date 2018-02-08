using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {Unidentified = -1, Carrot, Potato, Onion }

public class Plant : MonoBehaviour {

    public Type plantType = Type.Unidentified;
    public Sprite[] spriteCycle;
    public float growthSpeed;
    public float lightBoost;
    public float waterBoost;
    public float startHealth;

    public bool lighted = false;
    public bool watered = false;
    
    int currentPhase = 0;
    float health;
    float maxHealth;
    float growth = 0;
	
    void Start() {
        health = maxHealth = startHealth;
        if(spriteCycle != null)
            GetComponent<SpriteRenderer>().sprite = spriteCycle[0];
    }

    void Update() {
        growth += growthSpeed;
        if (lighted) growth += lightBoost;
        if (watered) growth += waterBoost;
        if (growth >= 100) {
            PhaseUp();
            growth = 0;
        }
    }

    void PhaseUp() {
        if (currentPhase == spriteCycle.Length - 1) return;
        currentPhase++;
        health += startHealth * 0.2f;
        maxHealth += startHealth * 0.2f;
        // spawn particles
        // play sound
        GetComponent<SpriteRenderer>().sprite = spriteCycle[currentPhase]; // swap sprite
        // max phase check & stop growing
    }

    public void GetDamage(float damage) {
        health += damage;
        // damage flash
        // play sound
        if (health < 0) Kill(); // death check
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
        // give product
        // spawn particles
        // play sound
        // destroy
    }
}