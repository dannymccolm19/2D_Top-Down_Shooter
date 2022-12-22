using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlienController : MonoBehaviour
{
    public GameObject player;
    int currentHealth;
    public int maxHealth = 20;
    public float speed;
    new Rigidbody2D rigidbody2D;
    float timer;

    bool paused;
    bool restart;
    public bool dead = false;
    public int damage;
    public Vector3 spawn;
    public GameObject[] parts;
    public double range;

    float distx = 100;
    float disty = 100;
    double dist = 100;
    PolygonCollider2D pCollider;

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        pCollider = GetComponent<PolygonCollider2D>();
        currentHealth = maxHealth;
        
    }
    void Update(){
        paused = HealthBar.instance.paused;
        if(paused){
            return;
        }
        restart = HealthBar.instance.restart;
        if(restart){
            rigidbody2D.isKinematic = false;
            currentHealth = maxHealth;
            transform.position = spawn;
            pCollider.enabled = true;
            foreach(GameObject part in parts)part.SetActive(true);
            dead = false;
        }
        if(currentHealth == 0){
            dead = true;
            rigidbody2D.isKinematic = true;
            //transform.eulerAngles = new Vector3(0f,0f, 0f);
            pCollider.enabled = false;
            foreach(GameObject part in parts)part.SetActive(false);
        }
        if(dead){return;}
        distx = (player.transform.position.x - transform.position.x);
        disty =  (player.transform.position.y - transform.position.y);
        dist = Math.Sqrt( (distx*distx) + (disty*disty) );
    
    }

    void FixedUpdate(){
        if(paused){
            return;
        }
        if(dead){return;}
        //if(dist == null){return;}
        Vector2 position = rigidbody2D.position;
        
        if(dist <range)
        {
            if(disty <0){
                transform.eulerAngles = new Vector3(0f,0f, (float)((Math.Asin(distx/dist))*180/Math.PI) + 180f);
            }
            else{
                transform.eulerAngles = new Vector3(0f,0f, ((float)((Math.Asin(distx/dist))*180/Math.PI)*-1f + 360f));
            }
            position.x = position.x + Time.deltaTime * speed * (distx/10);
            position.y = position.y + Time.deltaTime * speed * (disty/10);
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(dead)return;
        PlayerController p = other.collider.GetComponent<PlayerController>();
        if (p != null)
        {
            p.ChangeHealth(damage);
        }
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);     
    }
}
