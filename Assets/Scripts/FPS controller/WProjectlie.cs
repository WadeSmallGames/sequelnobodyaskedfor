using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WProjectlie : MonoBehaviour
{
    private Rigidbody rb;
    public float  launch;
    public float damage;
    private float timer;
    public float lifeTime;
    public ObjectPooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * launch;
        pooler = FindObjectOfType<ObjectPooler>().GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
    /*    timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(this);
        }*/
        
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
            pooler.QReset(gameObject);
        }
        
    }
}
