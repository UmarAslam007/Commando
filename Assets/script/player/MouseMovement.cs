using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField]
    private Transform PalyerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float Sensivity = 5f;

    [SerializeField]
    private int Smooth_step = 10;

    [SerializeField]
    private float Smooth_weight = 0.4f;

//z-axis roation
//    [SerializeField]
//    private float roll_angle = 10f;

//    [SerializeField]
//    private float rollSpeed = 3f;

    [SerializeField]
    private Vector2 default_lookLimits = new Vector2(-70f, 80f);

    private Vector2 lookAngles;

    private Vector2 current_Mouse_look;
    private Vector2 smothMove;

    private float currentRollAngle;

    private int lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        current_Mouse_look = new Vector2(
            Input.GetAxis(MouseAxis.Mouse_y),Input.GetAxis(MouseAxis.Mouse_X));

        lookAngles.x += current_Mouse_look.x * Sensivity * (invert ? 1f : -1f);
        lookAngles.y += current_Mouse_look.y * Sensivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, default_lookLimits.x, default_lookLimits.y);
     // z-axis roatation
        //   currentRollAngle =
        //   Mathf.Lerp(currentRollAngle, Input.GetAxis(MouseAxis.Mouse_y) * roll_angle, Time.deltaTime * rollSpeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        PalyerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);



    }
}
