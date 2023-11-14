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

    public void level1(){
        SceneManager.LoadScene("Movement101", LoadSceneMode.Single);
    }

    public void level2(){
        SceneManager.LoadScene("wallruns", LoadSceneMode.Single);
    }

    public void level3(){
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void level4(){
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    public void level5(){
        SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }


    public void level6(){
        SceneManager.LoadScene("Level4", LoadSceneMode.Single);
    }

    public void level7(){
        SceneManager.LoadScene("Level5", LoadSceneMode.Single);
    }

    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
