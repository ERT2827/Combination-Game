using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reticleScript : MonoBehaviour
{
    private void Awake() {
        StartCoroutine(selfDestruct());
    }


    IEnumerator selfDestruct(){
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}

