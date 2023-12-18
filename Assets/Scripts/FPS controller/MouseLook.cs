using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float xMousePos, smoothedMousePos, currentLookingPos;
    public float smoothing = 1.5f, sensitivity = 1.5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
     
    }

    public void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }
    public void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f/smoothing);

    }
    public void MovePlayer()
    {
        currentLookingPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
