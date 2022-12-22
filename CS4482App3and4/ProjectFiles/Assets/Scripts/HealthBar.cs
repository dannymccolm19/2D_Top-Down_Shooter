using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance { get; private set; }
    
    public Image mask;
    float originalSize;
    public bool restart = false;
    float timer = 0.0f;
    float waitTime = 1.0f;
    public bool paused = false;
    public int lang = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {				      
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(!restart){
            timer = 0;
        }
        if (timer>waitTime){
            restart = false;
        }
        
    }
}
