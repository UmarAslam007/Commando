using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private AudioSource jumpAudio;
    
    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 10f;

    public float jump_force = 10f;
    private float vertical_velocity;

    private void Awake()
    {
        characterController = GetComponent < CharacterController>();
        jumpAudio = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f,Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(move_Direction);
    }

    void ApplyGravity()
    {
        vertical_velocity -= gravity * Time.deltaTime;

        PlayerJump();

        vertical_velocity -= gravity * Time.deltaTime;

        move_Direction.y = vertical_velocity*Time.deltaTime;
    }

    void PlayerJump()
    {
        if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_velocity = jump_force;
            jumpAudio.Play();
        }
    }
}
