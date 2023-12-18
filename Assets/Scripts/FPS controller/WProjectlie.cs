using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WProjectlie : MonoBehaviour
{
    private Rigidbody rb;
    public float  launch;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * launch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider!= null)
        {
            if (collider.gameObject.GetComponent<Health>() != null)
            {
                Health health = collider.gameObject.GetComponent<Health>();
                health.TakeDamage(damage);
            }
            Destroy(rb.gameObject);
        }
        
    }
}
