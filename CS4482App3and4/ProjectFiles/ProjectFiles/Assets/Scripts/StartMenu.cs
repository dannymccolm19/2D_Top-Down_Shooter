using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public Button exit;
    public Button start;
    public TMP_Dropdown drop;
    public English english;
    public French french;
    public Spanish spanish;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.instance.paused = true;
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.instance.lang = drop.value;

        TMP_Text e = exit.GetComponentInChildren<TMP_Text>();
        TMP_Text s = start.GetComponentInChildren<TMP_Text>();

        switch(drop.value){
            case 0:
                e.text = english.Values[6];
                s.text = english.Values[5];
                break;
            case 1:
                e.text = french.Values[6];
                s.text = french.Values[5];
                break;
            case 2:
                e.text = spanish.Values[6];
                s.text = spanish.Values[5];
                break;
        }
    }

    void StartGame(){
        gameObject.SetActive(false);
        //player.Restart(false);
    }

    void ExitGame(){
        Application.Quit();
    }
}
