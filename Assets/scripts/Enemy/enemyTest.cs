using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    shootingScript shootS;


    private void Awake() {
        shootS = GameObject.Find("Player").GetComponent<shootingScript>();
    }
    
    public void die(){
        Debug.Log("Oh my god I'm dead!!!");
    }


    private void OnMouseDown() {
        if (!shootS.onCoolDown)
        {
            die();
        }
    }
}
