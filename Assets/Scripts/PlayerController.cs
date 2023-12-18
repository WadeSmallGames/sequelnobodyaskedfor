using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 wasd = new();
    Vector3 newPos = new();
    Rigidbody _rb;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5f;

    private void Start()
    {
        TryGetComponent(out _rb);   
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.A)) wasd.x = -1;
        if (Input.GetKey(KeyCode.S)) wasd.z = -1;
        if (Input.GetKey(KeyCode.D)) wasd.x = 1;
        if (Input.GetKey(KeyCode.W)) wasd.z = 1;
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) wasd.x = 0;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) wasd.z = 0;

        newPos = transform.position + wasd;
        Debug.DrawLine(transform.position, newPos, Color.red);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveSpeed);
    }

    bool inAir;
    void HandleJumpInput()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || inAir) return;


        _rb.AddForce(new(0, 1 * jumpForce, 0), ForceMode.Impulse);
        inAir = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        inAir = false;
    }
}
