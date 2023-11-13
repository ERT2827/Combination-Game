using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostScript : MonoBehaviour
{
    private playerController playerC;
    [SerializeField] bool boostType;
    
    private void OnTriggerEnter2D(Collider2D other) {
        playerC = other.GetComponent<playerController>();

        if(playerC != null){
            if(boostType){
                playerC.doubleJumped = false;
                playerC.boostedJump = true;
            }else
            {
                playerC.boostedSlide = true;
            }
        }
    }
}
