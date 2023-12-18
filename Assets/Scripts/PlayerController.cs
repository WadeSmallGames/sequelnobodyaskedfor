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
    Camera cam;

    private void Start()
    {
        TryGetComponent(out _rb);   
        cam = Camera.main;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleRotation();
        HandleGunFire();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.A)) wasd.x = -1;
        if (Input.GetKey(KeyCode.S)) wasd.z = -1;
        if (Input.GetKey(KeyCode.D)) wasd.x = 1;
        if (Input.GetKey(KeyCode.W)) wasd.z = 1;
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) wasd.x = 0;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) wasd.z = 0;

        _rb.velocity = wasd * moveSpeed;
        newPos = transform.position + wasd;
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

    
    enum AimMode { Free, LockOn}
    [Header("Shooting")]
    [SerializeField] AimMode _aim;
    Vector3 _target = new();
    [SerializeField] float bulletForce = 5000f;
    [SerializeField] float lockOnRange = 10f;
    void HandleGunFire()
    {
        Debug.DrawLine(transform.position, _target, Color.blue);

        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

        bool fire = false;
        if(_aim == AimMode.LockOn)
        {
            var array = Physics.OverlapSphere(transform.position, lockOnRange);
            var dict = new Dictionary<Transform, float>();
            if (array == null) return;

            foreach(var item in array)
                if(item.TryGetComponent(out EnemyVirtual enemy))
                    dict.Add(enemy.transform, (enemy.transform.position - transform.position).magnitude);

            KeyValuePair<Transform, float> closest = new(null, 1000f);
            foreach (KeyValuePair<Transform, float> kvp in dict)
                if (kvp.Value <= closest.Value)
                    closest = kvp; 

            if(closest.Key != null)
            {
                _target = closest.Key.position;
                fire = true;
            }
        }

        if (!fire) return;
        GetComponentInChildren<ObjectPooler>().Spawn(out Projectile projectile);
        projectile.Init(_target, bulletForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        inAir = false;
    }
}
