using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    //the stop distance to the player 
    public float stopDistance;

    private float attackTime;

    // animator reference for the current enemy
    private Animator anim;

    // enemy shot point transform
    public Transform shotPoint;

    //bullet for this enemy
    public GameObject enemyBullet;

    // Start is called before the first frame update

    public override void Start()
    {
        base.Start(); // the enemy start function

        // the animator component
        anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {
    
        if (player != null)
        {
            //if is not close enough to the player
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                // move towards it 
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= attackTime)
                    {
                    //the next time for the attack
                        attackTime = Time.time + timeBetweenAttack;
                    //chage the animation for the attack animation when is close to the player
                        anim.SetTrigger("attack");
                    }

            }
            

        }
    }

    // the function for the animation panel in Unity, for the time when the top of the weapon is up in the timeline
    public void RangedAttack()
    {
        if(player != null)

        {
            Vector2 direction = player.position - shotPoint.position; 

            //the angle the weapon must rotate around to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Calculam unghiul

            //Quaternion is a class that represents rotations in Unity
            //Vcetor3.forward is a vector pointing in the direction of the positive z-axis
            //Quaternion.AngleAxis creates a rotation which rotates angle degrees around the axis
            //We then set the rotation of the object to this rotation
            Quaternion rotation = Quaternion.AngleAxis(angle , Vector3.forward); //Rotim obiectul

            shotPoint.rotation = rotation; //Aplicam rotatia

            Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);

        }
        
    }
}
