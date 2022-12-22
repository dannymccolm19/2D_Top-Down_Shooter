using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIWeapons : MonoBehaviour
{
    public PlayerController player;
    public Image weapon1;
    public Image weapon2;
    public TMP_Text ammo1;
    public TMP_Text ammo2;
    void Start()
    {
        
    }

    
    void Update()
    {
        bool m = true;
        bool n = true;
        for(int i = 0; i<3;i++){
            if(player.rightWeapons[i]){        
                m = false;
                ammo2.text = player.currentAmmo[i] + "/" + player.maxAmmo[i];
                ammo2.fontSize = 60;
                String path = "Sprites/Weapon" + i;
                weapon2.sprite = Resources.Load<Sprite>(path);     
            }
        }
        if(m){
            weapon2.sprite = Resources.Load<Sprite>("Sprites/RightHand");
            ammo2.text = "∞";  
            ammo2.fontSize = 150;
        }

        for(int i = 0; i<3;i++){
            if(player.leftWeapons[i]){        
                n = false;
                ammo1.text = player.currentAmmo[i] + "/" + player.maxAmmo[i];
                ammo1.fontSize = 60;
                String path = "Sprites/Weapon" + i;
                weapon1.sprite = Resources.Load<Sprite>(path);  
                  
            }
        }
        if(n){
            weapon1.sprite = Resources.Load<Sprite>("Sprites/LeftHand");
            ammo1.text = "∞";  
            ammo1.fontSize = 150; 
        }
        
        weapon1.preserveAspect = true;
        weapon2.preserveAspect = true;
        //ammo1.preserveAspect = true;
        //ammo2.preserveAspect = true;
    }
}

