using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    // max and min for the scene coordinates
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;


    // the position where the summoner will spawn the minons
    private Vector2 targetPosition;

    // a reference to the animator for summoner
    private Animator anim;

    public float timeBetweenSummons;
    //when the minions will be spawned
    private float summonTime;

    //what to we want to spawn
    public Enemy enemyToSummon;


    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //we have the start function in the enemy script

        //random coordinates for the summoner new position
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);   
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position,targetPosition) > .5f) { 
                //moving to the target position
                transform.position = Vector2.MoveTowards(transform.position,targetPosition,speed * Time.deltaTime);
                //variable to go from the idle animation to the running animation
                anim.SetBool("isRunning", true);

            } else
            {
                //if is in  the idle state 
                anim.SetBool("isRunning", false);
                if(Time.time >= summonTime)
                {
                    // it will spawn minions when the time had past and is grater or equal to summonTime
                    summonTime = Time.time + timeBetweenSummons;
                    //the trigger for the summon animation ( spawn minions)
                    anim.SetTrigger("summon");
                     
                }
            }
            //if is close to the player it will attack it like the meleeEnemy does
            if (Vector2.Distance(transform.position, player.position) <=  stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    // used in the animation panel for making the enemy appear at the right time on the "summon" animation timeline.
    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().takeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;

        }
    }
}
