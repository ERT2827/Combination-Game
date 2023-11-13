using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public enum playerCurrentState{run, slide, jump, wallrun, wallkick}

public class playerController : MonoBehaviour
{
    [Header("Player Values")]
    public float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float speedStore;
    public int health;


    [Header("callable items")]
    private Rigidbody2D rb;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask stopLayers;


    [SerializeField] private float castDistance;

    public GameObject runningWall = null;

    [SerializeField] TMP_Text speedometer;

    private shootingScript shooter;

    TrailRenderer lightTrail;

    [Header("logic drivers")]
    public playerCurrentState currentState;

    public bool moving = true;
    bool isGrounded = true;
    bool doubleJumped = false;
    bool boostedSlide = false;
    bool firstSlide = true;
    public bool boostedDismount = false;

    [Header("side checks")]
    [SerializeField] private Vector2 sideBoxSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 stopBoxSize = new Vector2(1f, 6f);

    [SerializeField] private float sideCastDistance;
    [SerializeField] private float stopCastDistance;


    [SerializeField] public int wallKickDir = 0;

    public bool levelComplete = false;


    [Header("Touch Stuff")]
    Vector3 startTouchPos;
    Vector3 endTouchPos;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(resistence());

        rb = gameObject.GetComponent<Rigidbody2D>();

        shooter = gameObject.GetComponent<shootingScript>();

        lightTrail = gameObject.transform.GetChild(0).gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    
    void FixedUpdate(){
        if (!moving){
            speedX = 0;
        }else if(speedX < 10 && moving){
            speedX += 0.05f;
        }else if (speedX > 30){
            speedX = 30;
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Die();
        }

        speedometer.text = "Speed: " + Mathf.Round((speedX * 3.6f)).ToString() + "KM/H";
        
        
        groundCheck();
        wallCheck();
        
        rb.velocity = new Vector2(speedX, rb.velocity.y);

        #if UNITY_ANDROID

            if (Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began){
                startTouchPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPos = Input.GetTouch(0).position;

                if (Mathf.Abs(endTouchPos.y - startTouchPos.y) < 20 && Mathf.Abs(endTouchPos.x - startTouchPos.x) < 20)
                {
                    shooter.mobileshoot(startTouchPos);

                }else if (Mathf.Abs(endTouchPos.y - startTouchPos.y) > Mathf.Abs(endTouchPos.x - startTouchPos.x)){
                    if (endTouchPos.y > startTouchPos.y){
                        if (currentState == playerCurrentState.wallrun)
                        {
                            jump();
                        }else if (isGrounded){
                            jump();
                            Debug.Log(isGrounded);
                        }else if(!doubleJumped){
                            doubleJump();
                        }
                    }else if (endTouchPos.y < startTouchPos.y && currentState == playerCurrentState.run)
                    {
                        StartCoroutine(slide());
                    }
                }else{
                    if(endTouchPos.x > startTouchPos.x && runningWall != null && currentState != playerCurrentState.wallrun){
                        wallRun();
                    }else if(endTouchPos.x > startTouchPos.x && wallKickDir == 3){
                        wallKick(false);
                    }else if(endTouchPos.x < startTouchPos.x && wallKickDir == 3){
                        wallKick(true);
                    }else if(endTouchPos.x > startTouchPos.x && wallKickDir == 1){
                        wallKick(true);
                    }else if(endTouchPos.x < startTouchPos.x && wallKickDir == 2){
                        wallKick(false);
                    }
                }
            }   

        #endif

        #if UNITY_STANDALONE_WIN

        if (Input.GetButtonDown("Jump") && currentState == playerCurrentState.wallrun)
        {
            jump();
            currentState = playerCurrentState.jump;
        }else if (Input.GetButtonDown("Jump") && isGrounded){
            jump();
            Debug.Log(isGrounded);
        }else if(Input.GetButtonDown("Jump") && !doubleJumped){
            doubleJump();
        }


        if (Input.GetAxisRaw("Vertical") == -1 && currentState == playerCurrentState.run)
        {
            StartCoroutine(slide());
        }

        if(Input.GetAxisRaw("Horizontal") == 1 && wallKickDir == 3){
            wallKick(false);
        }else if(Input.GetAxisRaw("Horizontal") == -1 && wallKickDir == 3){
            wallKick(true);
        }else if(Input.GetAxisRaw("Horizontal") == -1 && wallKickDir == 1){
            wallKick(true);
        }else if(Input.GetAxisRaw("Horizontal") == 1 && wallKickDir == 2){
            wallKick(false);
        }
        
        if(Input.GetAxisRaw("Horizontal") == 1 && runningWall != null && currentState != playerCurrentState.wallrun){
            wallRun();
        }

        #endif
            
    }
    


    void jump(){
        if (boostedDismount){
            speedX += 10;
        }else if(currentState != playerCurrentState.slide){
            speedX -= 1;
        }else if(currentState == playerCurrentState.slide){
            speedX += 1;
        }

        float JP = 0 - rb.velocity.y;

        rb.velocity = new Vector2(speedX, JP + 8);
    }


    void doubleJump(){
        speedX += 2;

        float JP = 0 - rb.velocity.y;

        rb.velocity = new Vector2(speedX, JP + 5);

        doubleJumped = true;
    }

    void groundCheck(){
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer) && currentState == playerCurrentState.slide)
        {
            isGrounded = true;
            doubleJumped = false;
        }
        else if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){
            isGrounded = true;
            currentState = playerCurrentState.run;
            doubleJumped = false;
        }else{
            isGrounded = false;
        }

        if (runningWall == null || currentState != playerCurrentState.wallrun)
        {
            rb.gravityScale = 1;
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position+transform.up*castDistance, boxSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position+transform.right*sideCastDistance, sideBoxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position-transform.right*sideCastDistance, sideBoxSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position+transform.right*stopCastDistance, stopBoxSize);


    }

    void wallRun(){
        currentState = playerCurrentState.wallrun;

        speedX += 5;

        rb.gravityScale = 0;

        if(runningWall != null){
            transform.position = new Vector2(transform.position.x, runningWall.transform.position.y);
            rb.velocity = new Vector2(speedX, 0);
        }
    }
    
    void wallCheck(){
        if(Physics2D.BoxCast(transform.position, stopBoxSize, 0, transform.right, stopCastDistance, stopLayers) && currentState != playerCurrentState.slide){
            moving = false;
        }else{
            moving = true;
        }
        
        
        if(Physics2D.BoxCast(transform.position, sideBoxSize, 0, transform.right, sideCastDistance, wallLayer) && Physics2D.BoxCast(transform.position, sideBoxSize, 0, -transform.right, sideCastDistance, wallLayer)){
            wallKickDir = 3;
        }else if(Physics2D.BoxCast(transform.position, sideBoxSize, 0, transform.right, sideCastDistance, wallLayer)){
            wallKickDir = 1;
        }else if(Physics2D.BoxCast(transform.position, sideBoxSize, 0, -transform.right, sideCastDistance, wallLayer)){
            wallKickDir = 2;
        }else{
            wallKickDir = 0;
        }

    }

    void wallKick(bool direction){
        if(direction){
            rb.velocity = new Vector2(-5, 10);
        }else{
            rb.velocity = new Vector2(5, 10);
            speedX = speedStore + 5;
        }
    }

    public void Ptakedamage(){
        health -= 1;
        Debug.Log(health);
    }

    public void Die(){
        if(!levelComplete){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator resistence(){
        if(currentState == playerCurrentState.run && speedX > 10){
            speedX -= 1;
        }

        yield return new WaitForSeconds(0.4f);

        StartCoroutine(resistence());
    }

    IEnumerator slide(){
        currentState = playerCurrentState.slide;

        if(firstSlide){
            transform.localScale = new Vector2(transform.localScale.x, 0.5f);
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            firstSlide = false;
        }
        

        if(!boostedSlide && speedX <= 10){
            speedX = 10;
        }else if(boostedSlide){
            speedX += 10;
        }

        yield return new WaitForSeconds(1f);

        bool endCheck = Physics2D.BoxCast(transform.position, boxSize, 0, transform.up, castDistance, ceilingLayer);

        if(endCheck){
            StartCoroutine(slide());
        }else{
            currentState = playerCurrentState.run;
            transform.localScale = new Vector2(transform.localScale.x, 1f);
            firstSlide = true; 
        }

    }


}
