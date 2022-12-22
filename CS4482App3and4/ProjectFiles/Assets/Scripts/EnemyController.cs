using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 20;
    public float speed;
    public float changeTime = 3.0f;

    public GameObject player;
    public GameObject gun;
    public GameObject[] parts;
    public GameObject bullet;
    
    public bool vertical;
    public bool auto;
    public bool shotgun;
    public bool dead = false;
    public double range;
    public Vector3 spawn;
    
    float waitTime = 1.0f;
    float distx = 100;
    float disty = 100;
    double dist = 100;
    float reload = 2.0f;
    float timer;
    int direction = 1;
    int shots = 0;
    int maxShots = 10;

    bool paused;
    bool restart;

    new Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        timer = changeTime;
        currentHealth = maxHealth;
        
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
            rigidbody2D.isKinematic = false;
            currentHealth = maxHealth;
            gun.SetActive(true);  
            boxCollider.enabled = true; 
            transform.position = spawn;
            foreach(GameObject part in parts){part.SetActive(true);}
            dead = false;
        }
        if(currentHealth == 0){
            dead = true;
            rigidbody2D.isKinematic = true;
            //transform.eulerAngles = new Vector3(0f,0f, 0f);
            foreach(GameObject part in parts){part.SetActive(false);}
        }
        if(dead){return;}
        distx = (player.transform.position.x - transform.position.x);
        disty =  (player.transform.position.y - transform.position.y);
        dist = Math.Sqrt( (distx*distx) + (disty*disty) );
        timer += Time.deltaTime;
        
        if(dist <range)
        {
            return;
        }

        if (timer > changeTime)
        {
            direction = -direction;
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        if(paused){
            return;
        }
        if(dead){return;}
        Vector2 position = rigidbody2D.position;
        
        if(dist <range)
        {
            //timer += Time.deltaTime;
            if(disty <0){
                transform.eulerAngles = new Vector3(0f,0f, (float)((Math.Asin(distx/dist))*180/Math.PI) + 180f);
            }
            else{
                transform.eulerAngles = new Vector3(0f,0f, ((float)((Math.Asin(distx/dist))*180/Math.PI)*-1f + 360f));
            }
            if(auto){waitTime = 0.2f; maxShots = 15;}
            else if (shotgun){waitTime = 0.8f; maxShots = 4;}
            else {waitTime = 0.8f; maxShots = 10;}
            if(timer> waitTime)
            {
                if(shots > maxShots){
                    if(timer> reload)shots = 0;
                }
                else {
                    if(shotgun){Shotgun(distx, disty);}
                    else{Shoot(distx, disty);}
                    shots++;
                    timer = 0;
                }
            }
        }
        else if (vertical)
        {
            float dir;
           
            if(direction>0){
                dir = 0f;
            }
            else{
                dir = 180f;
            }
            position.y = position.y + Time.deltaTime * speed * direction;
            transform.eulerAngles = new Vector3(0f,0f, dir);
        }
        else
        {
            float dir;
            if(direction>0){
                dir = 270f;
            }
            else{
                dir = 90f;
            }
            position.x = position.x + Time.deltaTime * speed * direction;
            transform.eulerAngles = new Vector3(0f,0f, dir);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void Shoot(float x, float y){
        GameObject newBullet = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
        newBullet.transform.Rotate(0,0, transform.rotation.eulerAngles.z, Space.Self);
        EnemyShoot shoot = newBullet.GetComponent<EnemyShoot>();
        shoot.Launch(new Vector2(x,y), 500, false);
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);     
    }

    

    void Shotgun(float x, float y){
      
        GameObject newBullet1 = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
        GameObject newBullet2 = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);    
        GameObject newBullet3 = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
        GameObject newBullet4 = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);    
        GameObject newBullet5 = (GameObject) Instantiate(bullet, new Vector2(gun.transform.position.x, gun.transform.position.y), Quaternion.identity);
        EnemyShoot shoot1 = newBullet1.GetComponent<EnemyShoot>();
        EnemyShoot shoot2 = newBullet2.GetComponent<EnemyShoot>();
        EnemyShoot shoot3 = newBullet3.GetComponent<EnemyShoot>();
        EnemyShoot shoot4 = newBullet4.GetComponent<EnemyShoot>();
        EnemyShoot shoot5 = newBullet5.GetComponent<EnemyShoot>();
        shoot1.Launch(new Vector2(x,y), 600, true);
        
        if((distx*distx) < (disty*disty)){
            shoot2.Launch(new Vector2 (x + 2f, y ), 600, true);
            shoot3.Launch(new Vector2 (x - 2f, y ), 600, true);
            shoot4.Launch(new Vector2 (x + 1f, y), 600, true);
            shoot5.Launch(new Vector2 (x - 1f, y), 600, true);
        }       
        else{
            shoot2.Launch(new Vector2 (x , y + 2f), 600, true);
            shoot3.Launch(new Vector2 (x , y - 2f), 600, true);
            shoot4.Launch(new Vector2 (x , y + 1f), 600, true);
            shoot5.Launch(new Vector2 (x , y - 1f), 600, true);
        }
        
        
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController p = other.GetComponent<PlayerController >();

        if (p != null && dead)
        {
            int i;
            if(auto)i=1;
            else if(shotgun)i=2;
            else i=0;
            gameObject.SetActive(false); 
            p.AmmoPickup(i);

        }
    }*/

    public void DestroyEnemy()
    {
        gun.SetActive(false);  
        boxCollider.enabled = false;  
    }
}
