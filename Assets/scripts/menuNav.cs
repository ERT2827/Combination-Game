using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuNav : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Levels;
    
    public void quit(){
        Application.Quit();
    }

    public void openLevels(){
        Menu.SetActive(false);
        Levels.SetActive(true);
    }

    public void back(){
        Menu.SetActive(true);
        Levels.SetActive(false);
    }

    public void loadmenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void testLevel(){
        SceneManager.LoadScene("testingScene", LoadSceneMode.Single);
    }

    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
