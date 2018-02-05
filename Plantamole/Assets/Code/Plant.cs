using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    public string id;
    public float growthSpeed;
    public float lightBoost;
    public float waterBoost;
    public float startHealth;

    public bool lighted = false;
    public bool watered = false;

    // 0, 1, 2, 3, 4
    int phase = 0;
    float health;
    float maxHealth;
    float growth = 0;
	
    void Start() {
        health = maxHealth = startHealth;
    }

    void Update() {
        growth += growthSpeed;
        if (lighted) growth += lightBoost;
        if (watered) growth += waterBoost;
        if (growth%100 == 1) {
            PhaseUp();
            growth = 0;
        }
    }

    void PhaseUp() {
        phase++;
        health += startHealth * 0.2f;
        maxHealth += startHealth * 0.2f;
        // spawn particles
        // play sound
        // swap sprite
        // max phase check & stop growing
    }

    public void GetDamage(float damage) {
        health += damage;
        // damage flash
        // play sound
        // death check
    }

    public void Kill() {
        // spawn particles
        // destroy
    }

    public void HealUp(float heal) {
        health += heal;
        // spawn particle
        // max hp check
    }

    public void Harvest() {
        // give product
        // spawn particles
        // play sound
        // destroy
    }
}