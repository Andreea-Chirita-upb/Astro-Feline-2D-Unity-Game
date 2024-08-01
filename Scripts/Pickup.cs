


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
   
    public Weapon weaponToEquip;

    //the effect for the pickup
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //the pickup effect
            Instantiate(effect, transform.position, Quaternion.identity);
            //change the player weapon in the player script
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);


        }
    }
}
