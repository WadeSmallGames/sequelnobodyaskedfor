using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FpsController : MonoBehaviour
{
    public float speed = 20;
    private CharacterController cc;

    public Vector3 inputVector;
    public Vector3 movementVector;
    public float gravity = -10;
    private Animator animator;
    private bool isMoving;
    public float magni;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        CheckForBob();
        magni = cc.velocity.magnitude;
        animator.SetBool("Moving", isMoving);
        if (Input.GetMouseButtonDown(0)) Fire();

        
    }

    void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        movementVector = (inputVector * speed) + (Vector3.up * gravity);    
    }

    void MovePlayer()
    {
        cc.Move(movementVector * Time.deltaTime);
        
    }

    void CheckForBob()
    {
        if (cc.velocity.magnitude == 0)
        {

            isMoving = false;

        }
        else
        {
            isMoving = true;
        }
    }

    void Fire()
    {
        GetComponentInChildren<ObjectPooler>().Spawn(out Projectile projectile);
    }
}
