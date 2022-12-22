using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

using UnityEngine.Tilemaps;

public class Timer : MonoBehaviour
{
    public GameObject timer;
    public Guide guide;
    public TMP_Text textm;
    public float seconds = 0f;
    public float minutes = 0f;
    public int dies = 0;
    public bool paused = false;
    public bool beat;
    public EnemyController[] enemies;
    public AlienController[] aliens;
    public Turret[] turrets;
    public Tilemap tiles;
    //public StartMenu startMenu;

    public void Restart(){
        seconds = 0f;
        minutes = 0f;
        dies = 0;
        beat= false;
        for(int x = 1; x<6; x++){
            tiles.SetTile(new Vector3Int (x*-1, 15, 0), Resources.Load<Tile>("Tiles/tileset_24"));
        }
    }

    void Update()
    {
        int e = 0;
        int a = 0;
        int t = 0;
        foreach(EnemyController x in enemies){
            if(x.dead)
                e++;
        };
        foreach(AlienController x in aliens){
            if(x.dead)
                a++;
        };
        foreach(Turret x in turrets){
            if(x.dead)
                t++;
        };
        if(enemies.Length == e){
            for(int x = 1; x<6; x++){
                tiles.SetTile(new Vector3Int (x*-1, 15, 0), Resources.Load<Tile>("Tiles/tileset_28"));
            }
            if(aliens.Length == a && turrets.Length == t){
                beat= true;
                guide.index = 4;
                if(!HealthBar.instance.restart)guide.BeatGame();
            }
        }
        
        paused = HealthBar.instance.paused;
        if(!paused){
            timer.SetActive(true);
            seconds += Time.deltaTime;
            float second = (float)Math.Round(seconds * 100f)/100f ;
            if (seconds > 60){
                seconds -= 60;
                minutes +=1;
            }
            if(seconds < 10){
                textm.SetText(minutes.ToString() + ":0" + second.ToString() + "\n" + "Died: " + dies);
            }
            else{
                textm.SetText(minutes.ToString() + ":" + second.ToString() + "\n" + "Died: " + dies);
            }
        }
        else{
            timer.SetActive(false);
        }
    }

    public void Died (){
        dies++;
        for(int x = 1; x<6; x++){
            tiles.SetTile(new Vector3Int (x*-1, 15, 0), Resources.Load<Tile>("Tiles/tileset_24"));
        }
    }
}
