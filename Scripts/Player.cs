using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Player : MonoBehaviour
{
    public float speed; //This variable determines the speed of the player character and can be adjusted in the Unity editor.


    //component in Unity that is used to simulate physics in two dimensions, allowing GameObjects to interact with forces such as gravity, collisions, and applied impulses.
    private Rigidbody2D rb;
   

    private Vector2 moveAmount;

    // Animator component 
    private Animator anim;

    public int  health;

    //for weapon point
    public Transform weaponPoint;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Animator hurt;

    //public GameObject soundFootStep;
    private SceneTrasitions sceneTransitions;
    // Start is called before the first frame update
    void Start()
    {
        // Animator component attached to the Player 
        anim = GetComponent<Animator>();

        // Rigidbody component that is attached to our Player 
        rb = GetComponent<Rigidbody2D>();

        sceneTransitions = FindAnyObjectByType<SceneTrasitions>();
    }

    // Update is called once per frame
    void Update()
    {
        // The move input helps us determine which keys are pressed (up, down, left, right) corresponding to the moveInput coordinates (x,1) - up, (x,-1) - down, (1,-1) - right, (-1,-1) - left.

        //Vector2(x,y) - 2 coordinates
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")); 


        moveAmount = moveInput.normalized * speed;

        //check if the player is moving
        if(moveInput != Vector2.zero)
        {
            //bool variable for transition to idle to run
            anim.SetBool("isRunning", true);
           

        }
        else
        {
            //bool variable for transition to run to idle
            anim.SetBool("isRunning", false);
        }

    }
   // called every single frame
   
    public void FixedUpdate()
    {
        // change the position of the RigidBody with the correct amount from the input(key on the keyboard)
        //Time.fixedDeltaTime(to ensure consistent movement regardless of frame rate)
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void takeDamage(int damageAmount)
    {
        health = health - damageAmount;
        hurt.SetTrigger("hurt");
        //update the UI hearts in order to be the same as the player current health
        UpdateHealthUI(health);
        if (health <= 0)
        {

            Destroy(this.gameObject);
            sceneTransitions.LoadScene("Lose");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        //destroy the current weapon of the player
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        //change it with the pickup one
        ChangeWeapon1(weaponToEquip);
    }
    public void ChangeWeapon1(Weapon weaponToEquip)
    {
       //new player's weapon
        Instantiate(weaponToEquip, weaponPoint.position, weaponPoint.rotation, weaponPoint);
    }

    // UI update 
    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    //update player's health
    public void Heal(int healAmount)
    {

        if (health + healAmount <= hearts.Length)
        {
            health += healAmount;
        }
        else
            health = hearts.Length;

        //update Hearts UI with for the new health
        UpdateHealthUI(health);
        

    }
}
