using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField]float thrustspeed = 1000f;
    [SerializeField]float rotationspeed = 100f;

    Rigidbody rigidbody;
    AudioSource audioSource;
    
    // Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}
    private void Thrust()
    {
        float thrustthisframe = thrustspeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustthisframe);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotate()
    {
        rigidbody.freezeRotation = true;
        float rotationthisframe = rotationspeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationthisframe);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationthisframe);
        }
        rigidbody.freezeRotation = false;
    }
}
