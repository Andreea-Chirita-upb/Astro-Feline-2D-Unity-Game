using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    //enemies that will spawn after half of the life is killed
    public Enemy[] enemies;

    //the offset for the enemies from the boss position
    public float spawnOffset;

    private int halfHealth;
    //boss animator component
    private Animator anim;

    public int damage;
    //blood effect when it is killed
    public GameObject blood;
    //effect when it is killed
    public GameObject effect;

    //heaalth bar from the UI
    private Slider healthBar;

    private SceneTrasitions sceneTransitions;

    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindAnyObjectByType<SceneTrasitions>();
    }


    public void takeDamage(int damageAmount)
    {
        health = health - damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);

            Instantiate(effect, transform.position, Quaternion.identity);

            Destroy(this.gameObject);

            //make the boss's health bar invisible
            healthBar.gameObject.SetActive(false);
            //change scene when the boss is killed
            sceneTransitions.LoadScene("Win");
        }

        if(health <= halfHealth)
        {
            //running animation 
            anim.SetTrigger("Stage2");
        }

        //spawn enemies when is attacked
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];

        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().takeDamage(damage);
        }
    }
}
