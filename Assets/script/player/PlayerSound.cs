using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSound : MonoBehaviour
{
    private AudioSource footStepSound;

    [SerializeField]
    private AudioClip[] footstepclip;

    private CharacterController characterController;

    [HideInInspector]
    public float volumeMin, volumeMax;

    private float accumualtedDistance;

    [HideInInspector]
    public float stepDistance;

    private void Awake()
    {
        footStepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootStepSound();
    }

     void CheckToPlayFootStepSound()
    {
        if (!characterController.isGrounded)
            return;

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumualtedDistance += Time.deltaTime;

            if (accumualtedDistance > stepDistance)
            {
                footStepSound.volume = Random.Range(volumeMin, volumeMax);
                footStepSound.clip = footstepclip[Random.Range(0, footstepclip.Length)];
                footStepSound.Play();

                accumualtedDistance = 0f;
            }
        }
        else
        {
            accumualtedDistance = 0f;
        }
    }
}
