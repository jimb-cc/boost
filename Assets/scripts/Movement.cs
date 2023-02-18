using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
   
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThursterParticles;
    [SerializeField] ParticleSystem rightThursterParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!mainEngineParticles.isPlaying) mainEngineParticles.Play();
            
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if(!rightThursterParticles.isPlaying) rightThursterParticles.Play();

        }
        else if (Input.GetKey(KeyCode.D))
        {
           ApplyRotation(-rotationThrust);
            if(!leftThursterParticles.isPlaying) leftThursterParticles.Play();

        }
        else
        {
            rightThursterParticles.Stop();
            leftThursterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manualy rotate

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        rb.freezeRotation = false;  // pass control back to physics system

    }
}
