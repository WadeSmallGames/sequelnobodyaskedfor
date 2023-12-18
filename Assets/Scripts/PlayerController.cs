using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 wasd = new();
    Vector3 newPos = new();
    Rigidbody _rb;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float rotationSpeed = 3f;

    private void Start()
    {
        TryGetComponent(out _rb);   
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleRotation();
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

    void HandleRotation()
    {
        Vector3 dir = (newPos - transform.position).normalized;

        if (dir == Vector3.zero) return;

        float dot = Vector3.Dot(transform.forward, dir);
        dot = -dot;
        dot += 2;
        dot *= rotationSpeed;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * dot);
        Debug.DrawRay(transform.position, dir, Color.blue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        inAir = false;
    }
}
