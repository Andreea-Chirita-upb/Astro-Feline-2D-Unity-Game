using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrasitions : MonoBehaviour
{
    private Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    public void LoadScene(string SceneName)
    {
        StartCoroutine(Transition(SceneName));
    }
    //change the scene 
    IEnumerator Transition(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);

    }

}
