using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public Timer timer;
    public Button exit;
    public Button restart;
    public PlayerController player;
    public TMP_Dropdown drop;
    public English english;
    public French french;
    public Spanish spanish;
    //public static PauseMenu instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(RestartGame);
        exit.onClick.AddListener(ExitGame);
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.instance.lang = drop.value;
        TMP_Text e = exit.GetComponentInChildren<TMP_Text>();
        TMP_Text r = restart.GetComponentInChildren<TMP_Text>();
        switch(drop.value){
            case 0:
                e.text = english.Values[6];
                r.text = english.Values[7];
                break;
            case 1:
                e.text = french.Values[6];
                r.text = french.Values[7];
                break;
            case 2:
                e.text = spanish.Values[6];
                r.text = spanish.Values[7];
                break;
        }

        
    }

    void RestartGame(){
        gameObject.SetActive(false);
        HealthBar.instance.paused = false;
        Time.timeScale = 1;
        player.Restart(false);
    }

    public void ToggleMenu()
    {			
        if (gameObject.activeSelf){
            gameObject.SetActive(false);
            if(!player.guide.MenuStatus()){
                HealthBar.instance.paused = false;
                Time.timeScale = 1;
            }
        }	
        else{
            gameObject.SetActive(true);
            HealthBar.instance.paused = true;
            Time.timeScale = 0;

            drop.value = HealthBar.instance.lang;
        }      
    }

    public bool MenuStatus()
    {			
        return gameObject.activeSelf;      
    }

    void ExitGame(){
        Application.Quit();
    }
}
