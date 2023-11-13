using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    [Header("Timer Settings")]
    public float currentTime;
    private TimeSpan timePlayed;
    public bool running = true;

    private void Start() {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(running){
            currentTime += Time.deltaTime;
            timePlayed = TimeSpan.FromSeconds(currentTime);
            string timePlayedStr = timePlayed.ToString("mm':'ss'.'f");
            timerText.text = timePlayedStr;
        }
        
    }
}
