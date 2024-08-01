using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  
    //the points for the boss random movement
    private GameObject[] patrolPoints;
    public float speed;
    int randomPoint;
    private GameObject player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");

        randomPoint = Random.Range(0, patrolPoints.Length);

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null) { 
            //move the boss towards the random "patrol points"
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
                //choose another random point to move the boss
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
        }
    }


}
