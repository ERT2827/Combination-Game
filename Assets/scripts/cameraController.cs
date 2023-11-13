using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    
    [Header("Offset and general components")]
    [SerializeField] private Vector3 camOffSet = new Vector3(10, 2, -10);
    [SerializeField] private float offsetMultiplyerX = 0.1f;
    // [SerializeField] private float offsetMultiplyerY = 0.1f;

    private Vector3 currentOffSet;
    [SerializeField] private GameObject player;
    Vector3 playerPos;

    playerController playerC;

    [SerializeField] private Vector2 MaxOffSet = new Vector2(25, 10);
    [SerializeField] private Vector2 MinOffSet = new Vector2(4, 2);
    Vector3 velocity;
    [SerializeField] private float moveTime = 0.3f;

    
    [Header("Zoom Level")]
    private float zoomLevel;
    [SerializeField] private float zoomMultiplyer = 0.5f;
    [SerializeField] private float minZoom = 6;
    [SerializeField] private float maxZoom = 10;
    private float speed;
    [SerializeField] private float zoomTime = 0.25f;


    private Camera cam;

    private void Start() {
        cam = gameObject.GetComponent<Camera>();
        playerC = player.GetComponent<playerController>();

        zoomLevel = cam.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        zoom();
    }

    private void FixedUpdate() {
        playerPos = player.transform.position;
        
        OffSet(playerPos);
    }

    void zoom(){
        zoomLevel = playerC.speedX * zoomMultiplyer;
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
        
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomLevel, ref speed, zoomTime);

    }

    void OffSet(Vector3 PP){
        currentOffSet = new Vector3 (camOffSet.x + (playerC.speedX * offsetMultiplyerX), camOffSet.y + (playerC.speedX * offsetMultiplyerX), -10);
        currentOffSet = new Vector3 (Mathf.Clamp(currentOffSet.x, MinOffSet.x, MaxOffSet.x), Mathf.Clamp(currentOffSet.y, MinOffSet.y, MaxOffSet.y), -10);
        
        transform.position = Vector3.SmoothDamp(transform.position, (PP + currentOffSet), ref velocity, moveTime);
        // transform.position = PP + currentOffSet;
    }
}
