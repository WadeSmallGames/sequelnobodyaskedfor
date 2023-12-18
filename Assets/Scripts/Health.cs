using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Death();
            
        }
    }

    public void Death()
    {
        //do death suff
        Destroy(gameObject);
    }
}
