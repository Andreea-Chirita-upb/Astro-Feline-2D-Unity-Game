using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // a reference to the object the camera needs to follow
    public Transform playerTransform;
    public float speed;


    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        // camera and player are at the same position 
        transform.position = playerTransform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            //restrictions
            // the position cannot pass the min and the max X. if they to will assign the min or max . Min and Max are set in Unity Inspector for our camera 
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            //param : position, position we ant to move to , speed
            transform .position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
       
    }
}
