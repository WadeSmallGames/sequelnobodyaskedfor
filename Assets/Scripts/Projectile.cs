using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Init(Vector3 target, float force)
    {
        TryGetComponent(out Rigidbody rb);
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir;
        transform.LookAt(target);
        rb.AddForce(transform.forward * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
