using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] audioClips;


    private Movement movementScript;
    private float delayAmount = 1f;
    private bool isTransitioning = false;



    private void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

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
        isTransitioning = true;

        movementScript.audioSource.Stop();
        movementScript.audioSource.PlayOneShot(audioClips[0]);
        movementScript.DisableMovement();
        Invoke("ReloadLevel", delayAmount);

    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;

        movementScript.audioSource.Stop();
        movementScript.audioSource.PlayOneShot(audioClips[1]);
        movementScript.DisableMovement();
        Invoke("LoadNextLevel", delayAmount);
    }
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;


        SceneManager.LoadScene(nextSceneIndex);
    }
}
