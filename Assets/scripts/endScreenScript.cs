using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class endScreenScript : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] TMP_Text endTime;
    Timer timer;
    private TimeSpan timePlayed;


    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //needs to save
    }

    public void loadNextScene(){
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void setScreen(){
        timer = GameObject.Find("Player").GetComponent<Timer>();

        
        timer.running = true;

        timePlayed = TimeSpan.FromSeconds(timer.currentTime);
        string timePlayedStr = timePlayed.ToString("mm':'ss'.'f");
        endTime.text = timePlayedStr;
    }
}
