using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float thrustSpeed = 1f;
    [SerializeField]
    private float rotateSpeed = 1f;
    [SerializeField]
    private AudioClip engineClip;
    [SerializeField]
    private ParticleSystem[] paricleSystems;

    private Rigidbody rigidbodyRocket;
    public AudioSource audioSource;

    private void Start()
    {
        rigidbodyRocket = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        ProcessRotate();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineClip);
            }

            paricleSystems[0].Play();
            rigidbodyRocket.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
        }
        else
        {
            audioSource.Stop();
        }

    }

    private void ProcessRotate()
    {
        //FreezeRigidbodyRotation
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rigidbodyRocket.freezeRotation = false;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            rigidbodyRocket.freezeRotation = true;
        }


        //Rotation
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);

            paricleSystems[1].Play();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);


            paricleSystems[2].Play();

        }


    }

    private void ApplyRotation(float rotateThrust)
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThrust);
    }







    public void DisableMovement()
    {
        // audioSource.enabled = false;
        this.enabled = false;
    }






}
