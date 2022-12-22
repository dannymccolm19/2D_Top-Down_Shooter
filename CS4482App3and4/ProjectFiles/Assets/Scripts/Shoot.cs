using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    Rigidbody2D rigidbody2d;
    public int damage;
    bool shotgun;
    float timer = 0.0f;
    
    

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(shotgun){
            if(timer > 0.7){
                Destroy(gameObject);
            }
        }
        else{
            if(timer > 1.7){
                Destroy(gameObject);
            }
        }
        
    }

    public void Launch(Vector2 direction, float force, bool shot)
    {
        rigidbody2d.AddForce(direction * force);
        shotgun = shot;
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
    /*void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController e = other.GetComponent<EnemyController>();
        if (e != null)
        {
            e.ChangeHealth(damage);
        }
        Destroy(gameObject);
    }*/

    
}
