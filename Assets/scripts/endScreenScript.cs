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
    [SerializeField] private GameObject[] medalPrefs;
    [SerializeField] private Transform medSpawn;
    [SerializeField] private usableInformation savedData;


    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //needs to save
    }

    public void loadNextScene(){
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public void setScreen(){
        timer = GameObject.Find("Player").GetComponent<Timer>();

        
        timer.running = false;

        saveManager(savedData);
        savedData.saveInfo();

    }

    public void setMedal(int medLevel){
        

        if (medLevel > 0)
        {
            int i = medLevel - 1;
            Instantiate(medalPrefs[i], medSpawn.position, Quaternion.identity, medSpawn);
        }else{
            nextScene = "MainMenu";
        }
    }


    public void saveManager(usableInformation f){

        timePlayed = TimeSpan.FromSeconds(timer.currentTime);
        string timePlayedStr = timePlayed.ToString("mm':'ss'.'f");

        if(SceneManager.GetActiveScene().name == "Movement101"){
            if(f.bestTime1 > timer.currentTime){
                f.bestTime1 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "wallruns"){
            if(f.bestTime2 > timer.currentTime){
                f.bestTime2 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level1"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level2"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level3"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level4"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level5"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level6"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }else if(SceneManager.GetActiveScene().name == "Level7"){
            if(f.bestTime3 > timer.currentTime){
                f.bestTime3 = timer.currentTime;
                endTime.text = timePlayedStr + " NEW BEST";
            }else{
                endTime.text = timePlayedStr;
            }
        }
    }
}
