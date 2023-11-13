using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            return;
        }

        playerController playerc = other.gameObject.GetComponent<playerController>();
        playerc.Die();
    }
}
