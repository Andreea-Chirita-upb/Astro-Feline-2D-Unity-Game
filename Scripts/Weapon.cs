using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update


    // for shooting Projectiles
    public GameObject projectile;

    // the point from where are we shooting (position) that we will create on top of the weapon
    public Transform shotPoint;

    // the time for shooting the next bullet ( The time is needed to be past until shooting the next projectile ) 
    public float timeBetweenShots;

    // the time when we  shot the next bullet
    private float shotTime;

    //Animator component for the camera
    private Animator cameraAnim;
    
    void Start()
    {
        // the animator component for the camera
        cameraAnim = Camera.main.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    { 
        //The difference between the position of the mouse and the current position of the object.
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position; 
       

        //the angle the weapon must rotate around to face the cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

        //Quaternion is a class that represents rotations in Unity
        //Vcetor3.forward is a vector pointing in the direction of the positive z-axis
        //Quaternion.AngleAxis creates a rotation which rotates angle degrees around the axis
        //We then set the rotation of the object to this rotation
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); 

 
        transform.rotation = rotation; 




        //for shooting projectiles
        // GetMouseBuuton(i) -- i = 0 for left mouse buuton, i = 1 for right mouse button
        if (Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                // 1 parameter what we want to span, 2 param - where we want to span, 3 param - rotation
                Instantiate(projectile, shotPoint.position, transform.rotation);

                //we recalculte the time when we can shoot our next peojectile
                shotTime = Time.time + timeBetweenShots;

                // trigger for shake state when we shoot a projectile
                cameraAnim.SetTrigger("shake");
            }
        }
        
       
    }
}
