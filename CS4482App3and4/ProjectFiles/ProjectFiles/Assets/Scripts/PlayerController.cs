using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    float s;
    Vector2 p;
    float horizontal;
    float vertical;
    int currentHealth;
    public int maxHealth = 100;

    Rigidbody2D rigidbody2d;
    Animator animator;

    public Vector2 lookDirection = new Vector2(1,0);
    public bool vert;
    

    bool paused;
    
    public GameObject handL;
    public GameObject handR;
    SpriteRenderer handLSprite;
    SpriteRenderer handRSprite;
    public bool[] leftWeapons = {true, false, false};
    public bool[] rightWeapons = {false, false, false};
    bool burstL = false;
    bool burstR = false;
    int bL = 0;
    int bR = 0;

    int[] weaponCount = {1,0,0};

    public GameObject player;
    public GameObject[] bullets = new GameObject[9];
    public GameObject spark;
    public GameObject cross;

    public Guide guide;

    public PauseMenu menu;
    public Timer timer;

    public int[] maxAmmo = {40, 250, 15};
    public int[] currentAmmo = {40, 250, 15};
    

    float waitTime = 0.1f;
    float timerL = 0.0f;
    float timerR = 0.0f;
    float speedTimer = 0.0f;


    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        handLSprite = handL.GetComponent<SpriteRenderer>();
        handRSprite = handR.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        //handRSprite.sprite = Resources.Load<Sprite>("Sprites/Weapon1");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            menu.ToggleMenu();
        }
        if(Input.GetKeyDown(KeyCode.G) && !menu.MenuStatus() && !timer.beat){
            guide.ToggleGuide();
        }
        paused = HealthBar.instance.paused;
        if(paused){
            return;
        }
        
        timerL += Time.deltaTime;
        timerR += Time.deltaTime;
        speedTimer += Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
      

        lookDirection.Set((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - rigidbody2d.position.x), (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rigidbody2d.position.y));
        lookDirection.Normalize();
        if(Math.Abs(lookDirection.x) > Math.Abs(lookDirection.y)){
            vert = false;
        }
        else{
            vert = true;
        }


        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);


        cross.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
        
        if(speedTimer > 0.01f){
            speedTimer = 0;
            s =  (rigidbody2d.position.x*rigidbody2d.position.y) - (p.x*p.y);
        }
        if (Input.GetMouseButton(0) && leftWeapons[1]){
            timerL += Time.deltaTime;
            if(timerL> 0.2f)
            {
                Shoot(1, handL);
                timerL = 0;
            }
        }
        else if(Input.GetMouseButtonDown(0) && timerL> 0.2f){
            timerL = 0;
            if(leftWeapons[0]){
                burstL = true;
            }
            else if(leftWeapons[2]){
                Shotgun(handL);
            }
            else{
            GameObject tempSpark = (GameObject) Instantiate(spark, new Vector2(handL.transform.position.x, handL.transform.position.y), Quaternion.identity);
            }          
        }
        
        

        if (Input.GetMouseButton(1) && rightWeapons[1]){
            timerR += Time.deltaTime;
            if(timerR> 0.2f)
            {
                Shoot(1, handR);
                timerR = 0;
            }
        }
        else if(Input.GetMouseButtonDown(1) && timerR> 0.2f){
            timerR = 0;
            if(rightWeapons[0]){
                burstR = true;
            }
            else if(rightWeapons[2]){
                Shotgun(handR);
            }
            else{
            GameObject tempSpark = (GameObject) Instantiate(spark, new Vector2(handR.transform.position.x, handR.transform.position.y), Quaternion.identity);
            }
             
        }
       
        
        if(burstL){
            if(timerL> waitTime)
            {
                Shoot(0, handL);
                timerL = 0;
                bL++;
            }
            if(bL >= 3){
                bL =0;
                burstL =false;
            }
        }
        if(burstR){
            if(timerR> waitTime)
            {
                Shoot(0, handR);
                timerR = 0;
                bR++;
            }
            if(bR >= 3){
                bR =0;
                burstR =false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            int i = 0;
            if(leftWeapons[0]){
                leftWeapons[0] = false;
                if((weaponCount[1] == 1 && !rightWeapons[1]) || weaponCount[1] == 2){
                    i = 1;
                }
                else if((weaponCount[2] == 1 && !rightWeapons[2]) || weaponCount[2] == 2){
                    i = 2;
                }
                else{  
                    handLSprite.sprite = null; 
                    i = 7;
                }
            }
            else if(leftWeapons[1]){
                leftWeapons[1] = false;
                if((weaponCount[2] == 1 && !rightWeapons[2]) || weaponCount[2] == 2){ 
                    i = 2;
                }
                else{  
                    handLSprite.sprite = null; 
                    i = 7;
                }
            }
            else if(leftWeapons[2]){
                leftWeapons[2] = false;
                handLSprite.sprite = null; 
                i = 7;       
            }
            if (i!=7){
                leftWeapons[i] = true;
                String path = "Sprites/Weapon" + i;
                handLSprite.sprite = Resources.Load<Sprite>(path);
            }
           

            
        }
        
        if(Input.GetKeyDown(KeyCode.E)){
            
            int i = 0;
            if(rightWeapons[0]){
                rightWeapons[0] = false;
                if((weaponCount[1] == 1 && !leftWeapons[1]) || weaponCount[1] == 2){
                    i = 1;
                }
                else if((weaponCount[2] == 1 && !leftWeapons[2]) || weaponCount[2] == 2){
                    i = 2;
                }
                else{  
                    handRSprite.sprite = null; 
                    i = 7;
                }
            }
            else if(rightWeapons[1]){
                rightWeapons[1] = false;
                if((weaponCount[2] == 1 && !leftWeapons[2]) || weaponCount[2] == 2){ 
                    i = 2;
                }
                else{  
                    handRSprite.sprite = null; 
                    i = 7;
                }
            }
            else if(rightWeapons[2]){
                rightWeapons[2] = false;
                handRSprite.sprite = null; 
                i = 7;       
            }
            if (i!=7){
                rightWeapons[i] = true;
                String path = "Sprites/Weapon" + i;
                handRSprite.sprite = Resources.Load<Sprite>(path);
            }
            
        }

       

        
        
    }

    void FixedUpdate()
    {
        if(paused){
            return;
        }
        Vector2 position = rigidbody2d.position;
        p = position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
        
    }

    void Shoot(int i, GameObject hand){
        
        if(currentAmmo[i]>0){
            currentAmmo[i]--;
        }
        else{return;}
            
        
        GameObject newBullet = (GameObject) Instantiate(bullets[i], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);
        newBullet.transform.Rotate(0,0, player.transform.rotation.eulerAngles.z, Space.Self);
        Shoot shoot = newBullet.GetComponent<Shoot>();
        if(Mathf.Abs(s) > 0) shoot.Launch(lookDirection, 700,  false);
        else shoot.Launch(lookDirection, 500,  false);
        
        
    }

    void Shotgun(GameObject hand){
        
        if(currentAmmo[2]>0){
            currentAmmo[2]--;
        }
        else{return;}
        GameObject newBullet1 = (GameObject) Instantiate(bullets[2], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);
        GameObject newBullet2 = (GameObject) Instantiate(bullets[2], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);    
        GameObject newBullet3 = (GameObject) Instantiate(bullets[2], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);
        GameObject newBullet4 = (GameObject) Instantiate(bullets[2], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);    
        GameObject newBullet5 = (GameObject) Instantiate(bullets[2], new Vector2(hand.transform.position.x, hand.transform.position.y), Quaternion.identity);
        Shoot shoot1 = newBullet1.GetComponent<Shoot>();
        Shoot shoot2 = newBullet2.GetComponent<Shoot>();
        Shoot shoot3 = newBullet3.GetComponent<Shoot>();
        Shoot shoot4 = newBullet4.GetComponent<Shoot>();
        Shoot shoot5 = newBullet5.GetComponent<Shoot>();
        shoot1.Launch(lookDirection, 600, true);
        if(vert){
            shoot2.Launch(new Vector2 (lookDirection.x + 0.1f, lookDirection.y), 600, true);
            shoot3.Launch(new Vector2 (lookDirection.x - 0.1f, lookDirection.y), 600, true);
            shoot4.Launch(new Vector2 (lookDirection.x + 0.05f, lookDirection.y), 600, true);
            shoot5.Launch(new Vector2 (lookDirection.x - 0.05f, lookDirection.y), 600, true);
        }       
        else{
            shoot2.Launch(new Vector2 (lookDirection.x , lookDirection.y+ 0.1f), 600, true);
            shoot3.Launch(new Vector2 (lookDirection.x , lookDirection.y- 0.1f), 600, true);
            shoot4.Launch(new Vector2 (lookDirection.x , lookDirection.y+ 0.05f), 600, true);
            shoot5.Launch(new Vector2 (lookDirection.x , lookDirection.y- 0.05f), 600, true);
        }
        
        
    }

    public void ChangeHealth(int amount)
    {
        
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        HealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if(currentHealth == 0){
            Restart(true);
        }
        
    }

    public void Restart (bool died){
        currentHealth = maxHealth;
        player.transform.position = new Vector3 (-2.02f, -0.81f,0);
        for (int i = 0; i<3; i++){
            currentAmmo[i] = maxAmmo[i];
            leftWeapons[i] = false; 
            rightWeapons[i] = false;
            weaponCount[i] = 0;
        }
        leftWeapons[0] = true;
        weaponCount[0] = 1;
        handRSprite.sprite = null;
        handLSprite.sprite = Resources.Load<Sprite>("Sprites/Weapon0");
        HealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        HealthBar.instance.restart = true;
        if(died)timer.Died();
        else timer.Restart();
        
        //turret.Restart();
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        
        Turret t = other.collider.GetComponent<Turret>();
        if (t != null && t.dead)
        {
            int i;
            if(t.auto)i=1;
            else i = 0;
            if(weaponCount[i]<2)weaponCount[i]++;
           
            t.DestroyTurret();
            currentAmmo[i] = maxAmmo[i];
            
               
        } 
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null && e.dead)
        {
            int i;
            if(e.auto)i=1;
            else if(e.shotgun)i=2;
            else i = 0;
            if(weaponCount[i]<2)weaponCount[i]++;
           
            e.DestroyEnemy();
            currentAmmo[i] = maxAmmo[i];
            
             
        } 
        
        
    }

    

    
}
