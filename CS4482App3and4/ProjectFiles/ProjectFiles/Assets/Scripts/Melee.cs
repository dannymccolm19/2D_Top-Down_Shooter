using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    //Rigidbody2D rigidbody2d;
    float waitTime = 0.5f;
    float timer = 0.0f;
    public int damage;

    void Awake()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer> waitTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        Turret t = other.collider.GetComponent<Turret>();
        if (t != null)
        {
            t.ChangeHealth(damage);
        }
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.ChangeHealth(damage);
        }
        AlienController a = other.collider.GetComponent<AlienController>();
        if (a != null)
        {
            a.ChangeHealth(damage);
        }
        Destroy(gameObject);
        
        
    }

    

}
