using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbodyRocket;

    [SerializeField]
    private float thrustSpeed = 1f;
    [SerializeField]
    private float rotateSpeed = 1f;

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
            rigidbodyRocket.AddRelativeForce(Vector3.up * thrustSpeed * Time.fixedDeltaTime);
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }


    }
    
    private void ApplyRotation(float rotateThrust)
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThrust);
    }
}
