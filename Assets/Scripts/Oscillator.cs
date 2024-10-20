using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float periods = 2f; 

    private const float tau = Mathf.PI * 2; //tau value of 6.283

    private Vector3 startingPosition;
    private float movementFactor;



    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (periods <= Mathf.Epsilon) return;

        float cycles = Time.time / periods;  //continually growing over time

        float rawSinWave = Mathf.Sin(cycles * tau); //going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalulate to go from 0 to 1


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
