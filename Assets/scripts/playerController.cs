using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerCurrentState{run, slide, jump, wallrun, wallkick}

public class playerController : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float speedStore;

    [Header("callable items")]
    private Rigidbody2D rb;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float castDistance;

    public GameObject runningWall = null;

    [Header("booleans")]
    bool isGrounded = true;
    bool doubleJumped = false;
    bool isSliding = false;
    bool boostedSlide = false;
    bool firstSlide = true;
    bool wallRunning = false;


    [Header("side checks")]
    [SerializeField] private Vector2 sideBoxSize = new Vector2(1f, 1f);
    [SerializeField] private float sideCastDistance;
    [SerializeField] public int wallKickDir = 0;




    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(resistence());

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void FixedUpdate(){
        if(wallKickDir != 0){
            speedStore = speedX;
            speedX = 0;
        }else if(speedStore != 0){
            speedX = speedStore;
        }else if(speedX < 10 && wallKickDir == 0){
            speedX += 0.05f;
        }else if (speedX > 50){
            speedX = 50;
        }
    }
    
    void Update()
    {
        groundCheck();
        wallCheck();
        
        rb.velocity = new Vector2(speedX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && wallRunning)
        {
            jump();
            wallRunning = false;
        }else if (Input.GetButtonDown("Jump") && isGrounded){
            jump();
            Debug.Log(isGrounded);
        }else if(Input.GetButtonDown("Jump") && !doubleJumped){
            doubleJump();
        }


        if (Input.GetAxisRaw("Vertical") == -1 && !isSliding)
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
        
        if(Input.GetAxisRaw("Horizontal") == 1 && runningWall != null){
            wallRun();
        }
            
    }
    


    void jump(){
        if(!isSliding){
            speedX -= 3;
        }else if(isSliding){
            speedX += 3;
        }

        float JP = 0 - rb.velocity.y;

        rb.velocity = new Vector2(speedX, JP + 5);
    }


    void doubleJump(){
        speedX += 5;

        float JP = 0 - rb.velocity.y;

        rb.velocity = new Vector2(speedX, JP + 5);

        doubleJumped = true;
    }

    void groundCheck(){
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){
            isGrounded = true;
            doubleJumped = false;
        }else{
            isGrounded = false;
        }

        if (runningWall == null || !wallRunning)
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
    }

    void wallRun(){
        wallRunning = true;

        speedX += 2;

        rb.gravityScale = 0;

        if(runningWall != null){
            transform.position = new Vector2(transform.position.x, runningWall.transform.position.y);
            rb.velocity = new Vector2(speedX, 0);
        }
    }

    void wallCheck(){
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
            rb.velocity = new Vector2(-10, 10);
        }else{
            rb.velocity = new Vector2(10, 10);
        }
    }

    IEnumerator resistence(){
        if(isGrounded && !isSliding && speedX > 10){
            speedX -= 1;
        }

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(resistence());
    }

    IEnumerator slide(){
        isSliding = true;

        if(firstSlide){
            transform.localScale = new Vector2(transform.localScale.x, 0.5f);
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
            firstSlide = false;
        }
        

        if (!boostedSlide && speedX > 8){
            speedX -= 0.7f;
        }else if(!boostedSlide && speedX <= 8){
            speedX = 8;
        }

        yield return new WaitForSeconds(1f);

        bool endCheck = Physics2D.BoxCast(transform.position, boxSize, 0, transform.up, castDistance, ceilingLayer);

        if(endCheck){
            StartCoroutine(slide());
        }else{
            isSliding = false;
            transform.localScale = new Vector2(transform.localScale.x, 1f);
            firstSlide = true; 
        }

    }


}
