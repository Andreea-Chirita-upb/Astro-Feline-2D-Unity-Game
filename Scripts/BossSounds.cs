using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSounds : MonoBehaviour
{
    //reference to a audio source
    private AudioSource source;

    //arrays of clips for the boss
    public AudioClip[] clips;

    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime;
    // Start is called before the first frame update

    private void Start()
    {
        // boss audio source component
        source = GetComponent<AudioSource>();
    }  
    // Update is called once per frame
    void Update()
    {
        //change the sound animation after a period of time
        if(Time.time >= nextSoundEffectTime)
        {
            //radom clip
            int randomNumber = Random.Range(0, clips.Length);

            source.clip = clips[randomNumber];

            source.Play();
            //the new changing sound time
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
       
    }

  
    
}
