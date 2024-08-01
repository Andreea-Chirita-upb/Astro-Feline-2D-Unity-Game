using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // the distance when the enemy will stop and attack the player
    public float stopDistance;
    public float attackSpeed;

    float attackTime;


    // Update is called once per frame
    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance) {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                //when is not running and wait a amountOFTime to attack
                if(Time.time >= attackTime)
                {
                    //attack
                    // we will use corotine because 
                    // the animation is more than 1 frame to finish
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        //when is attacking the player , the player will have the health variable change in takeDamage function.
        player.GetComponent<Player>().takeDamage(damage);

        // the position of the enemy
        Vector2 originalPosition = transform.position;
        //the position of the player
        Vector2 targetPosition = player.position;

        // how much have we done of the animation
        // the percentage of completion of the movement
        float percent = 0;

        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            // move forward and return
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            //function to interpolate between the originalPosition (starting position) and the targetPosition (destination).
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;

        }
    }
}
