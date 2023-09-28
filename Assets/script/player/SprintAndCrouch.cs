using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_speed = 10f;
    public float move_speed = 5f;
    public float crouch_speed = 2f;

    private Transform look_root;
    private float stand_hight = 1.6f;
    private float crouch_hight = 1f;

    private bool is_crouch;

    private PlayerSound playerSound;

    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volumeMin = 0.2f, walk_voumeMax = 0.6f;

    private float walk_stepDistance = 0.4f;
    private float sprint_stepDistance = 0.25f;
    private float crouch_stepDistance = 0.5f;

    private PlayerStats playerStats;

    private float sprintValue = 100f;
    public float sprintTreshHolder = 10f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        
        look_root = transform.GetChild(0);
        playerSound = GetComponentInChildren<PlayerSound>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Start()
    {
        playerSound.volumeMin = walk_volumeMin;
        playerSound.volumeMax = walk_voumeMax;
        playerSound.stepDistance = walk_stepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        //if we have stamina
        if (sprintValue > 0f)
        {
            if (Input.GetKeyDown(KeyCode.RightControl) && !is_crouch  )
            {
                playerMovement.speed = sprint_speed;

                playerSound.stepDistance = sprint_stepDistance;
                playerSound.volumeMin = sprint_volume;
                playerSound.volumeMax = sprint_volume;

            }
        }
        if(Input.GetKeyUp(KeyCode.RightControl) && !is_crouch)
        {
            playerMovement.speed = move_speed;

            playerSound.stepDistance = walk_stepDistance;
            playerSound.volumeMin = walk_volumeMin;
            playerSound.volumeMax = walk_voumeMax;
            
        }

        if (Input.GetKey(KeyCode.RightControl) && !is_crouch)
        {
            sprintValue -= sprintTreshHolder * Time.deltaTime;

            if (sprintValue <= 0f)
            {
                sprintValue = 0f;

                playerMovement.speed = move_speed;

                playerSound.stepDistance = walk_stepDistance;
                playerSound.volumeMin = walk_volumeMin;
                playerSound.volumeMax = walk_voumeMax;
            }

            playerStats.DisplaySataminaStats(sprintValue);

        }
        else
        {
            if (sprintValue != 100f)
            {
                sprintValue += (sprintTreshHolder / 2f) * Time.deltaTime;

                playerStats.DisplaySataminaStats(sprintValue);
                
                if (sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
        }
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //if crouch standup
            if (is_crouch)
            {
                look_root.localPosition = new Vector3(0f, stand_hight, 0f);
                playerMovement.speed = move_speed;

                playerSound.stepDistance = walk_stepDistance;
                playerSound.volumeMin = walk_volumeMin;
                playerSound.volumeMax = walk_voumeMax;

                is_crouch = false;
            }
            //if not  crouch then crouch
            else
            {
                look_root.localPosition = new Vector3(0f, crouch_hight, 0f);
                playerMovement.speed = crouch_speed;

                playerSound.stepDistance = crouch_stepDistance;
                playerSound.volumeMin = crouch_volume;
                playerSound.volumeMax = crouch_volume;

                is_crouch = true;

            }
        }
    }

   
}//class
