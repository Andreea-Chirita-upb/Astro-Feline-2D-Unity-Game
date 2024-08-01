using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //health of the enemy
    public int health;

    //speed of the enemy
    public float speed;

    //the between the enemy attacks
    public float timeBetweenAttack;


    //the damage for each player to another gameObjects
    public int damage;

    //the effect of particles when a enemy dies
    public GameObject gameEffect;

    [HideInInspector]
    public Transform player;


    // the chance for a weapon to be instantiated
    public int pickupChance;

    // all weapons for the pickups
    public GameObject[] pickups;

    // the chance for a heart to be instantiated
    public int healthPickupChance;

    // heart for the pickups
    public GameObject healthPickup;

    //after it dies i will instantiate a "blood" gameObject


    public virtual void Start()
    {
        //the reference of the player (Player has the Player tag in the Unity Inspector

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }



    public void takeDamage(int damageAmount)
    {
        health = health - damageAmount;
        if(health <= 0)
        {
            
            int randomNumber= UnityEngine.Random.Range(0, 101);

            if(randomNumber < pickupChance)
            {
                // we will randomly choose a weapon to spawn when the enemy is killed
                GameObject randomPickup = pickups[UnityEngine.Random.Range(0,pickups.Length)];

               
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            //the chance that a heartPick is chosen
            // the change is set in the inspector, the same as the weapon pickup chance is set above
            int randHealth = UnityEngine.Random.Range(0, 101);
            if (randHealth <  healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            // Killed effect when the enemy dies
            // gameEffect is set in the inspector
            Instantiate(gameEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    
}
