using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    //the speed of the bullet
    public float speed;

    // how long the bullet will stay in  the game before it distroied
    public float lifeTime;

    // game object for explosion particles
    public GameObject explosion;

    // damage amount for attached to each weapon 
    public int damageAmount;

    public GameObject sound;

    void Start()
    {
        // the function will be called after the "lifeTime" time
        Invoke("DestroyProjectile", lifeTime);
        // sound for each time a projectile appear in the game scene
        Instantiate(sound, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // move the projectile up with a specific speed
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //param: what are you spawing, at what position, at what rotation
        Instantiate(explosion, transform.position, Quaternion.identity);

        // destroy projectile after the particles effect
        Destroy(gameObject);

    }

    //collision - the object that the gameObject is in collision with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // when the projectile hits a enemy we will call the funciton for setting the new health variable for it
            collision.GetComponent<Enemy>().takeDamage(damageAmount);

            DestroyProjectile();
        }
        if (collision.tag == "Boss")
        {
            // when the projectile hits the boss we will call the funciton for setting the new health variable for it
            collision.GetComponent<Boss>().takeDamage(damageAmount);
            DestroyProjectile();
        }


    }
}
