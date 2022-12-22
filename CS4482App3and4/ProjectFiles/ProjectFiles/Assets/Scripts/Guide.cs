using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guide : MonoBehaviour
{
    public English english;
    public French french;
    public German german;
    public Spanish spanish;
    public Italian italian;
    public KeyValues keyValues;

    public GameObject next;
    public GameObject back;
    public GameObject restart;
    public PlayerController player;
    public Timer timer;

    public TMP_Text textm;
    public int index = 0;

    int lang;
    // Start is called before the first frame update
    void Start()
    {
        next.GetComponent<Button>().onClick.AddListener(Next);
        back.GetComponent<Button>().onClick.AddListener(Back);
        restart.GetComponent<Button>().onClick.AddListener(Restart);
        HealthBar.instance.paused = true;
    }

    void Next (){
        index = keyValues.responseLink2[index];
        
    }

    void Back(){
        index = keyValues.responseLink1[index];
    }
    void Restart(){
        player.Restart(false);
        index = 0;
        ToggleGuide();
    }

    public void BeatGame()
    {			
        
        gameObject.SetActive(true);
        HealthBar.instance.paused = true;
        Time.timeScale = 0;
            
    }

    public void ToggleGuide()
    {			
        if (gameObject.activeSelf){
            gameObject.SetActive(false);
            HealthBar.instance.paused = false;
            Time.timeScale = 1;
        }	
        else{
            gameObject.SetActive(true);
            HealthBar.instance.paused = true;
            Time.timeScale = 0;
        }      
    }

    public bool MenuStatus()
    {			
        return gameObject.activeSelf;      
    }

    // Update is called once per frame
    void Update()
    {
        

        lang = HealthBar.instance.lang;
        string message = "";
        bool [] active = new bool[3];
        string[] responses = new string [3];
        active[0] = keyValues.active1[index];
        active[1] = keyValues.active2[index];
        active[2] = keyValues.active3[index];
        switch (lang){
            case 0:
                message = english.Values[index];
                responses[0] = english.response1[index];
                responses[1] = english.response2[index];
                responses[2] = english.response3[index];
                break;
            case 1:
                message = french.Values[index];
                responses[0] = french.response1[index];
                responses[1] = french.response2[index];
                responses[2] = french.response3[index];
                break;
            case 2:
                message = spanish.Values[index];
                responses[0] = spanish.response1[index];
                responses[1] = spanish.response2[index];
                responses[2] = spanish.response3[index];
                break;
            case 3:
                message = german.Values[index];
                responses[0] = german.response1[index];
                responses[1] = german.response2[index];
                responses[2] = german.response3[index];
                break;
            case 4:
                message = italian.Values[index];
                responses[0] = italian.response1[index];
                responses[1] = italian.response2[index];
                responses[2] = italian.response3[index];
                break;
        }

        TMP_Text b = back.GetComponentInChildren<TMP_Text>();
        if(active[0]){
            back.SetActive(true);
            b.text = responses[0];
        }
        else{
            back.SetActive(false);
        }
        TMP_Text n = next.GetComponentInChildren<TMP_Text>();
        if(active[1]){
            next.SetActive(true);
            n.text = responses[1];
        }
        else{
            next.SetActive(false);
        }
        TMP_Text r = restart.GetComponentInChildren<TMP_Text>();
        if(active[2]){
            restart.SetActive(true);
            r.text = responses[2];
        }
        else{
            restart.SetActive(false);
        }
        
       if(index ==4){
            message = message  + "\n" + "Time" + timer.textm.text;
       }
        textm.text =  message;
    }
}
