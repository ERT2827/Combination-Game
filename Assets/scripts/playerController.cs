using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

    [Header("callable items")]
    private Rigidbody2D rb;
    [SerializeField] private Vector2 boxSize = new Vector2(1f, 1f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ceilingLayer;

    [SerializeField] private float castDistance;

    [Header("booleans")]
    bool isGrounded = true;
    bool doubleJumped = false;
    bool isSliding = false;
    bool boostedSlide = false;
    bool firstSlide = true;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(resistence());

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void FixedUpdate(){
        if(speedX < 10){
            speedX += 0.05f;
        }else if (speedX > 50)
        {
            speedX = 50;
        }
    }
    
    void Update()
    {
        groundCheck();
        
        rb.velocity = new Vector2(speedX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded){
            jump();
            Debug.Log(isGrounded);
        }else if(Input.GetButtonDown("Jump") && !doubleJumped){
            doubleJump();
        }


        if (Input.GetKeyDown("s") && !isSliding)
        {
            StartCoroutine(slide());
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
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position+transform.up*castDistance, boxSize);
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
