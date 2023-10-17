using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float timer;
    [SerializeField] private float cooldown;

    [SerializeField] private float maxRange;
    public bool inRange = false;
    GameObject player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
        if(!inRange){
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance <= maxRange){
                inRange = true;
            }
        }
        

        timer += Time.deltaTime;

        // if(timer > cooldown && LES != null){
        //     if(LES.iscutscene){
        //         return;
        //     }else{
        //         timer = 0;
        //         shoot();
        //     }

        //in case we put in cutscenes
        if(timer > cooldown && inRange){
            timer = 0;
            shoot();
        }
    }


    void shoot(){
        Instantiate(bullet, shootPoint.position, Quaternion.identity);
    }
}
