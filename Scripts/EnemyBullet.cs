using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update

    // reference to the Player Object
    private Player playerScript;

    private Vector2 targetPosition;
    //damage of the bullet
    public int damage;

    public float speed;

    public GameObject effect;
    void Start()
    {
        //player gameObject
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //position of the player
        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);

        } else
        {
            //the effect when the bullet is destroyed
            Instantiate(effect, transform.position, Quaternion.identity);   
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // th
        if(collision.tag == "Player")
        {
            // update the player health
            playerScript.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
