using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootingScript : MonoBehaviour
{
    [SerializeField] private GameObject reticlePref;
    [SerializeField] private GameObject hitPref;

    public bool onCoolDown;

    [SerializeField] private float coolDownTime;

    
    SpriteRenderer sRnd;

    private void Awake() {
        sRnd = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate() {
        if (Input.GetMouseButton(0) && !onCoolDown)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -5f; // zero z
            Instantiate(reticlePref, mouseWorldPos, Quaternion.identity);

            StartCoroutine(coolDown());
        }
    }

    IEnumerator coolDown(){
        onCoolDown = true; 

        yield return new WaitForSeconds(coolDownTime);

        onCoolDown = false;
    }

}
