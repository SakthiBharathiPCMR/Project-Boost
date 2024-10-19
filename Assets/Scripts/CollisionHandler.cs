using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] 
    private Movement movementScript;

    private float delayAmount = 1f;

    private void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    Debug.Log("Its your friend");
                }
                break;
            case "Finish":
                {
                    StartSuccessSequence();
                }
                break;
            case "Fuel":
                {
                    Debug.Log("Its time reacharge the fuel");
                }
                break;
            default:
                {
                    //Debug.Log("you are in non tagged object");
                    StartCrashSequence();
                }
                break;
        }
    }

    private void StartCrashSequence()
    {
        movementScript.DisableMovement();
        Invoke("ReloadLevel", delayAmount);

    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    private void StartSuccessSequence()
    {
        movementScript.DisableMovement();
        Invoke("LoadNextLevel", delayAmount);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
            nextSceneIndex = 0;


        SceneManager.LoadScene(nextSceneIndex);
    }
}
