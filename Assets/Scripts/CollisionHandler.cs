using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private ParticleSystem[] paricleSystems;


    private Movement movementScript;
    private float delayAmount = 1f;
    private bool isTransitioning = false;
    private bool isCollisionDisabled = false;



    private void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCollisionDisabled) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                {
                    //Debug.Log("Its your friend");
                }
                break;
            case "Finish":
                {
                    StartSuccessSequence();
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
        paricleSystems[0].Play();
        movementScript.DisableMovement();
        Invoke("ReloadLevel", delayAmount);

    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;

        movementScript.audioSource.Stop();
        movementScript.audioSource.PlayOneShot(audioClips[1]);
        paricleSystems[1].Play();
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
