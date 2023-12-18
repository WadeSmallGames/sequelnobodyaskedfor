using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetFollow : MonoBehaviour
{
    public static Vector3 camPosOffset = new(0, 10.25f, -12);

    Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        transform.LookAt(player.position);
    }
    private void LateUpdate()
    {
        transform.position = player.position + camPosOffset;
        
    }
}
