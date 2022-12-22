using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
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
            if(timer > 0.5){
                Destroy(gameObject);
            }
        }
        else{
            if(timer > 1.5){
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
        
        PlayerController p = other.collider.GetComponent<PlayerController>();
        if (p != null)
        {
            p.ChangeHealth(damage);
        }
        Destroy(gameObject);
        
    }

    

    
}
