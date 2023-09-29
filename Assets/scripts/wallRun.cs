using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    playerController playa;
    SpriteRenderer spriteRenderer;
    GameObject parant;

    private void Start() {
        parant = gameObject.transform.parent.gameObject;
        spriteRenderer = parant.GetComponent<SpriteRenderer>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        playa = other.GetComponent<playerController>();

        spriteRenderer.color = Color.yellow;

        if(playa != null){
            playa.runningWall = parant;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        playa = other.GetComponent<playerController>();

        spriteRenderer.color = Color.grey;


        if (playa != null){}
        {
            playa.runningWall = null;
        }
    }
}
