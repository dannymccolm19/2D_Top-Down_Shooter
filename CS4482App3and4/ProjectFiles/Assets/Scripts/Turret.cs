using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    int currentHealth;
    public int maxHealth = 20;
    public GameObject bullet;
    float timer = 0.0f;
    float waitTime = 0.8f;
    float reload = 1.0f;
    bool restart;
    bool paused;
    public bool auto;
    int shots = 0;
    public GameObject part;
    public bool dead = false;
    public double range;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        paused = HealthBar.instance.paused;
        if(paused){
            return;
        }
        restart = HealthBar.instance.restart;
        if(restart){
            currentHealth = maxHealth;
            dead = false;
            boxCollider.enabled = true;
            gun.SetActive(true); 
            part.SetActive(true);
        }
        if(currentHealth == 0){
            dead = true;
            part.SetActive(false);
        }
        if(dead){return;}
        if(currentHealth > 0){
            //timer += Time.deltaTime;
            float distx = (player.transform.position.x - transform.position.x);
            float disty =  (player.transform.position.y - transform.position.y);
            double dist = Math.Sqrt( (distx*distx) + (disty*disty) );
            
            if(dist <range){
                timer += Time.deltaTime;
                if(disty <0){
                    gun.transform.eulerAngles = new Vector3(0f,0f, (float)((Math.Asin(distx/dist))*180/Math.PI) + 180f);
                }
                else{
                    gun.transform.eulerAngles = new Vector3(0f,0f, ((float)((Math.Asin(distx/dist))*180/Math.PI)*-1f + 360f));
                }
                if(auto)waitTime = 0.1f;
                else waitTime = 0.8f;
                if(timer> waitTime)
                {
                    
                    if(shots > 15){
                        if(timer> reload)shots = 0;
                    }
                    else {
                        Shoot(distx, disty);
                        shots++;
                        timer = 0;
                    }
                }
            }
            
        } 
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);     
    }

    public void DestroyTurret()
    {
        gun.SetActive(false);
        boxCollider.enabled = false;    
    }


    void Shoot(float distx, float disty){
        GameObject newBullet = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
        newBullet.transform.Rotate(0,0, gun.transform.rotation.eulerAngles.z, Space.Self);
        EnemyShoot shoot = newBullet.GetComponent<EnemyShoot>();
        shoot.Launch(new Vector2(distx,disty), 500, false);
        
    }
}
